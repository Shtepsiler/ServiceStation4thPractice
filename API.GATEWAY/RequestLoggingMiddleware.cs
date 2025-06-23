using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace API.GATEWAY
{
    // Enhanced Request Logging Middleware
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private readonly RequestLoggingOptions _options;

        public RequestLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger,
            IOptions<RequestLoggingOptions> options)
        {
            _next = next;
            _logger = logger;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = GetOrCreateCorrelationId(context);
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var originalBodyStream = context.Response.Body;

            // Capture request information
            var requestInfo = await CaptureRequestInfoAsync(context, correlationId);

            // Log incoming request
            LogRequest(requestInfo);

            try
            {
                // Capture response body if enabled
                using var responseBody = new MemoryStream();
                if (_options.LogResponseBody)
                {
                    context.Response.Body = responseBody;
                }

                await _next(context);

                sw.Stop();

                // Capture response information
                var responseInfo = await CaptureResponseInfoAsync(context, responseBody, originalBodyStream, sw.ElapsedMilliseconds);

                // Log response
                LogResponse(requestInfo, responseInfo);
            }
            catch (Exception ex)
            {
                sw.Stop();
                LogException(requestInfo, ex, sw.ElapsedMilliseconds);
                throw;
            }
        }

        private string GetOrCreateCorrelationId(HttpContext context)
        {
            const string correlationIdHeader = "X-Correlation-ID";

            if (context.Request.Headers.TryGetValue(correlationIdHeader, out var correlationId))
            {
                return correlationId.FirstOrDefault() ?? Guid.NewGuid().ToString();
            }

            var newCorrelationId = Guid.NewGuid().ToString();
            context.Request.Headers[correlationIdHeader] = newCorrelationId;
            context.Response.Headers[correlationIdHeader] = newCorrelationId;

            return newCorrelationId;
        }

        private async Task<RequestInfo> CaptureRequestInfoAsync(HttpContext context, string correlationId)
        {
            var request = context.Request;
            var connection = context.Connection;

            var requestInfo = new RequestInfo
            {
                CorrelationId = correlationId,
                Timestamp = DateTime.UtcNow,
                Method = request.Method,
                Path = request.Path.Value,
                QueryString = request.QueryString.Value,
                Scheme = request.Scheme,
                Host = request.Host.Value,
                UserAgent = request.Headers.UserAgent.FirstOrDefault(),
                ContentType = request.ContentType,
                ContentLength = request.ContentLength,
                RemoteIpAddress = GetClientIpAddress(context),
                UserName = context.User?.Identity?.Name,
                UserId = context.User?.FindFirst("sub")?.Value ?? context.User?.FindFirst("id")?.Value,
                Headers = _options.LogRequestHeaders ? GetSafeHeaders(request.Headers) : null,
                Cookies = _options.LogCookies ? GetSafeCookies(request.Cookies) : null
            };

            // Capture request body if enabled and appropriate
            if (_options.LogRequestBody && ShouldLogBody(request))
            {
                requestInfo.Body = await ReadRequestBodyAsync(request);
            }

            return requestInfo;
        }

        private async Task<ResponseInfo> CaptureResponseInfoAsync(
            HttpContext context,
            MemoryStream responseBody,
            Stream originalBodyStream,
            long elapsedMilliseconds)
        {
            var response = context.Response;

            var responseInfo = new ResponseInfo
            {
                StatusCode = response.StatusCode,
                ContentType = response.ContentType,
                ContentLength = response.ContentLength,
                ElapsedMilliseconds = elapsedMilliseconds,
                Headers = _options.LogResponseHeaders ? GetSafeHeaders(response.Headers) : null
            };

            // Copy response body back to original stream and capture if enabled
            if (_options.LogResponseBody && responseBody.Length > 0)
            {
                responseBody.Seek(0, SeekOrigin.Begin);
                responseInfo.Body = await new StreamReader(responseBody).ReadToEndAsync();

                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }

            context.Response.Body = originalBodyStream;
            return responseInfo;
        }

        private void LogRequest(RequestInfo requestInfo)
        {
            using var scope = _logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = requestInfo.CorrelationId,
                ["RequestId"] = Activity.Current?.Id ?? requestInfo.CorrelationId
            });

            var logLevel = GetLogLevel(requestInfo.Path);

            _logger.Log(logLevel,
                "HTTP {Method} {Scheme}://{Host}{Path}{QueryString} started. " +
                "RemoteIP: {RemoteIp}, UserAgent: {UserAgent}, User: {UserName}, " +
                "ContentType: {ContentType}, ContentLength: {ContentLength}",
                requestInfo.Method,
                requestInfo.Scheme,
                requestInfo.Host,
                requestInfo.Path,
                requestInfo.QueryString,
                requestInfo.RemoteIpAddress,
                requestInfo.UserAgent,
                requestInfo.UserName ?? "Anonymous",
                requestInfo.ContentType,
                requestInfo.ContentLength);

            // Log additional details at debug level
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                if (requestInfo.Headers?.Any() == true)
                {
                    _logger.LogDebug("Request Headers: {@Headers}", requestInfo.Headers);
                }

                if (!string.IsNullOrEmpty(requestInfo.Body))
                {
                    _logger.LogDebug("Request Body: {Body}", requestInfo.Body);
                }
            }
        }

        private void LogResponse(RequestInfo requestInfo, ResponseInfo responseInfo)
        {
            using var scope = _logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = requestInfo.CorrelationId,
                ["RequestId"] = Activity.Current?.Id ?? requestInfo.CorrelationId
            });

            var logLevel = GetLogLevel(requestInfo.Path, responseInfo.StatusCode);
            var statusCategory = GetStatusCategory(responseInfo.StatusCode);

            _logger.Log(logLevel,
                "HTTP {Method} {Path} responded {StatusCode} {StatusCategory} in {ElapsedMs}ms. " +
                "ContentType: {ContentType}, ContentLength: {ContentLength}, User: {UserName}",
                requestInfo.Method,
                requestInfo.Path,
                responseInfo.StatusCode,
                statusCategory,
                responseInfo.ElapsedMilliseconds,
                responseInfo.ContentType,
                responseInfo.ContentLength,
                requestInfo.UserName ?? "Anonymous");

            // Log performance warning for slow requests
            if (responseInfo.ElapsedMilliseconds > _options.SlowRequestThresholdMs)
            {
                _logger.LogWarning("Slow request detected: {Method} {Path} took {ElapsedMs}ms",
                    requestInfo.Method, requestInfo.Path, responseInfo.ElapsedMilliseconds);
            }

            // Log additional details at debug level
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                if (responseInfo.Headers?.Any() == true)
                {
                    _logger.LogDebug("Response Headers: {@Headers}", responseInfo.Headers);
                }

                if (!string.IsNullOrEmpty(responseInfo.Body))
                {
                    _logger.LogDebug("Response Body: {Body}", responseInfo.Body);
                }
            }
        }

        private void LogException(RequestInfo requestInfo, Exception exception, long elapsedMilliseconds)
        {
            using var scope = _logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = requestInfo.CorrelationId,
                ["RequestId"] = Activity.Current?.Id ?? requestInfo.CorrelationId
            });

            _logger.LogError(exception,
                "HTTP {Method} {Path} failed with exception after {ElapsedMs}ms. " +
                "RemoteIP: {RemoteIp}, User: {UserName}",
                requestInfo.Method,
                requestInfo.Path,
                elapsedMilliseconds,
                requestInfo.RemoteIpAddress,
                requestInfo.UserName ?? "Anonymous");
        }

        private string GetClientIpAddress(HttpContext context)
        {
            // Check for forwarded IP first (behind proxy/load balancer)
            var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                return forwardedFor.Split(',')[0].Trim();
            }

            var realIp = context.Request.Headers["X-Real-IP"].FirstOrDefault();
            if (!string.IsNullOrEmpty(realIp))
            {
                return realIp;
            }

            return context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        }

        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            try
            {
                request.EnableBuffering();
                var buffer = new byte[Convert.ToInt32(request.ContentLength ?? 0)];
                await request.Body.ReadAsync(buffer, 0, buffer.Length);
                request.Body.Position = 0;

                return System.Text.Encoding.UTF8.GetString(buffer);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to read request body");
                return "[Failed to read body]";
            }
        }

        private bool ShouldLogBody(HttpRequest request)
        {
            if (request.ContentLength > _options.MaxBodySizeToLog)
                return false;

            var contentType = request.ContentType?.ToLowerInvariant();
            if (contentType == null)
                return false;

            return contentType.Contains("application/json") ||
                   contentType.Contains("application/xml") ||
                   contentType.Contains("text/");
        }

        private Dictionary<string, string> GetSafeHeaders(IHeaderDictionary headers)
        {
            var safeHeaders = new Dictionary<string, string>();
            var sensitiveHeaders = new[] { "authorization", "cookie", "x-api-key", "x-auth-token" };

            foreach (var header in headers)
            {
                var key = header.Key.ToLowerInvariant();
                if (sensitiveHeaders.Contains(key))
                {
                    safeHeaders[header.Key] = "[REDACTED]";
                }
                else
                {
                    safeHeaders[header.Key] = string.Join(", ", header.Value.ToArray());
                }
            }

            return safeHeaders;
        }

        private Dictionary<string, string> GetSafeCookies(IRequestCookieCollection cookies)
        {
            var safeCookies = new Dictionary<string, string>();
            var sensitiveCookies = new[] { "bearer", "auth", "session", "token" };

            foreach (var cookie in cookies)
            {
                var key = cookie.Key.ToLowerInvariant();
                if (sensitiveCookies.Any(s => key.Contains(s)))
                {
                    safeCookies[cookie.Key] = "[REDACTED]";
                }
                else
                {
                    safeCookies[cookie.Key] = cookie.Value;
                }
            }

            return safeCookies;
        }

        private LogLevel GetLogLevel(string path, int? statusCode = null)
        {
            // Don't log health checks and metrics at info level
            if (path?.StartsWith("/health") == true || path?.StartsWith("/metrics") == true)
            {
                return LogLevel.Debug;
            }

            if (statusCode.HasValue)
            {
                return statusCode.Value switch
                {
                    >= 500 => LogLevel.Error,
                    >= 400 => LogLevel.Warning,
                    _ => LogLevel.Information
                };
            }

            return LogLevel.Information;
        }

        private string GetStatusCategory(int statusCode)
        {
            return statusCode switch
            {
                >= 200 and < 300 => "Success",
                >= 300 and < 400 => "Redirect",
                >= 400 and < 500 => "Client Error",
                >= 500 => "Server Error",
                _ => "Unknown"
            };
        }
    }

    // Configuration options
    public class RequestLoggingOptions
    {
        public bool LogRequestHeaders { get; set; } = false;
        public bool LogResponseHeaders { get; set; } = false;
        public bool LogRequestBody { get; set; } = false;
        public bool LogResponseBody { get; set; } = false;
        public bool LogCookies { get; set; } = false;
        public int MaxBodySizeToLog { get; set; } = 64 * 1024; // 64KB
        public int SlowRequestThresholdMs { get; set; } = 20000; // 20 seconds
    }

    // Data models
    public class RequestInfo
    {
        public string CorrelationId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string Method { get; set; } = string.Empty;
        public string? Path { get; set; }
        public string? QueryString { get; set; }
        public string Scheme { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public string? UserAgent { get; set; }
        public string? ContentType { get; set; }
        public long? ContentLength { get; set; }
        public string RemoteIpAddress { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string? UserId { get; set; }
        public Dictionary<string, string>? Headers { get; set; }
        public Dictionary<string, string>? Cookies { get; set; }
        public string? Body { get; set; }
    }

    public class ResponseInfo
    {
        public int StatusCode { get; set; }
        public string? ContentType { get; set; }
        public long? ContentLength { get; set; }
        public long ElapsedMilliseconds { get; set; }
        public Dictionary<string, string>? Headers { get; set; }
        public string? Body { get; set; }
    }

    // Extension method for easy registration
    public static class RequestLoggingExtensions
    {
        public static IServiceCollection AddRequestLogging(
            this IServiceCollection services,
            Action<RequestLoggingOptions>? configure = null)
        {
            if (configure != null)
            {
                services.Configure(configure);
            }
            else
            {
                services.Configure<RequestLoggingOptions>(_ => { });
            }

            return services;
        }

        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
