using JOBS.BLL.DTOs.Requests;
using JOBS.BLL.DTOs.Respponces;

namespace JOBS.BLL.Helpers
{
    class PartsMLApiClient : ApiHttpClient
    {
        public PartsMLApiClient(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <summary>
        /// Predict auto parts based on problem description
        /// </summary>
        /// <param name="request">Prediction request</param>
        /// <returns>Prediction response with parts and confidence scores</returns>
        public async Task<PredictionResponse> PredictPartsAsync(PredictionRequest request)
        {
            return await PostAsync<PredictionRequest, PredictionResponse>("/predict", request);
        }

        /// <summary>
        /// Submit feedback for a prediction
        /// </summary>
        /// <param name="feedback">Feedback data</param>
        /// <returns>Feedback submission result</returns>
        public async Task<FeedbackResponse> SubmitFeedbackAsync(FeedbackRequest feedback)
        {
            return await PostAsync<FeedbackRequest, FeedbackResponse>("/feedback", feedback);
        }

        /// <summary>
        /// Get API health status
        /// </summary>
        /// <returns>Health information</returns>
        public async Task<HealthResponse> GetHealthAsync()
        {
            return await GetAsync<HealthResponse>("/health");
        }

        /// <summary>
        /// Get model status and statistics
        /// </summary>
        /// <returns>Model status information</returns>
        public async Task<ModelStatus> GetModelStatusAsync()
        {
            return await GetAsync<ModelStatus>("/status");
        }

        /// <summary>
        /// Get detailed metrics for monitoring
        /// </summary>
        /// <returns>Metrics data</returns>
        public async Task<MetricsResponse> GetMetricsAsync()
        {
            return await GetAsync<MetricsResponse>("/metrics");
        }

        /// <summary>
        /// Get uncertain samples for human labeling
        /// </summary>
        /// <param name="limit">Maximum number of samples to return</param>
        /// <returns>Uncertain samples for active learning</returns>
        public async Task<UncertainSamplesResponse> GetUncertainSamplesAsync(int limit = 10)
        {
            var parameters = new Dictionary<string, string>
            {
                { "limit", limit.ToString() }
            };

            return await GetAsync<UncertainSamplesResponse>("/uncertain-samples", parameters);
        }

        /// <summary>
        /// Process multiple predictions in batch
        /// </summary>
        /// <param name="request">Batch prediction request</param>
        /// <returns>Batch prediction results</returns>
        public async Task<BatchPredictionResponse> PredictBatchAsync(BatchPredictionRequest request)
        {
            return await PostAsync<BatchPredictionRequest, BatchPredictionResponse>("/predict/batch", request);
        }

        /// <summary>
        /// Quick prediction with minimal parameters
        /// </summary>
        /// <param name="problemDescription">Problem description text</param>
        /// <param name="userId">Optional user identifier</param>
        /// <returns>Prediction response</returns>
        public async Task<PredictionResponse> QuickPredictAsync(string problemDescription, string userId = null)
        {
            var request = new PredictionRequest
            {
                ProblemDescription = problemDescription,
                UserId = userId,
                TopK = 5,
                ConfidenceThreshold = 0.1f
            };

            return await PredictPartsAsync(request);
        }

        /// <summary>
        /// Check if the ML API is available and healthy
        /// </summary>
        /// <returns>True if API is healthy, false otherwise</returns>
        public async Task<bool> IsHealthyAsync()
        {
            try
            {
                var health = await GetHealthAsync();
                return health.Status == "healthy" || health.Status == "degraded";
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get simple prediction result as key-value pairs
        /// </summary>
        /// <param name="problemDescription">Problem description</param>
        /// <returns>Dictionary of part name to confidence score</returns>
        public async Task<Dictionary<string, double>> GetSimplePredictionAsync(string problemDescription)
        {
            var response = await QuickPredictAsync(problemDescription);

            var result = new Dictionary<string, double>();
            foreach (var prediction in response.SimplePredictions)
            {
                foreach (var kvp in prediction)
                {
                    result[kvp.Key] = kvp.Value;
                }
            }

            return result;
        }
    }
}