namespace JOBS.BLL.Common.Exceptions
{
    public class MLApiException : Exception
    {
        public string ErrorCode { get; set; }
        public Dictionary<string, object> Details { get; set; }

        public MLApiException(string message) : base(message) { }
        public MLApiException(string message, Exception innerException) : base(message, innerException) { }
        public MLApiException(string message, string errorCode, Dictionary<string, object> details = null)
            : base(message)
        {
            ErrorCode = errorCode;
            Details = details;
        }
    }
}
