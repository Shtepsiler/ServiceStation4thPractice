using JOBS.BLL.DTOs.Respponces;

namespace JOBS.BLL.Helpers
{
    public static class MLApiClientExtensions
    {
        /// <summary>
        /// Get the most confident prediction
        /// </summary>
        public static PredictionItem GetTopPrediction(this PredictionResponse response)
        {
            return response.Predictions?.FirstOrDefault();
        }

        /// <summary>
        /// Check if prediction confidence is above threshold
        /// </summary>
        public static bool IsConfident(this PredictionResponse response, float threshold = 0.7f)
        {
            return response.ConfidenceScore >= threshold;
        }

        /// <summary>
        /// Get predictions above confidence threshold
        /// </summary>
        public static List<PredictionItem> GetConfidentPredictions(this PredictionResponse response, float threshold = 0.5f)
        {
            return response.Predictions?.Where(p => p.Confidence >= threshold).ToList() ?? new List<PredictionItem>();
        }

        /// <summary>
        /// Format prediction for display
        /// </summary>
        public static string FormatPrediction(this PredictionItem item)
        {
            return $"{item.PartName} ({item.Confidence:P1})";
        }

        /// <summary>
        /// Convert to simple dictionary format
        /// </summary>
        public static Dictionary<string, double> ToSimpleDictionary(this PredictionResponse response)
        {
            var result = new Dictionary<string, double>();
            foreach (var item in response.Predictions ?? new List<PredictionItem>())
            {
                result[item.PartName] = item.Confidence;
            }
            return result;
        }
    }

}
