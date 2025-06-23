using Newtonsoft.Json.Linq;
using System.Numerics;

namespace ServiceCenterPayment
{
    public class EthereumPriceConverter
    {
        private static readonly HttpClient httpClient = new HttpClient();
        public static decimal? Price;
        public static async Task<BigInteger> ConvertUsdToEtherAsync(decimal usdAmount, int decimals)
        {
            if (Price == null)
                //     Price = await GetEthereumPriceAsync();
                Price = 2357.50M;

            decimal ethAmount = usdAmount / Price.Value;

            BigInteger weiAmount = ConvertToWei(ethAmount, decimals);
            return weiAmount;
        }

        private static async Task<decimal> GetEthereumPriceAsync()
        {
            string url = "https://api.coingecko.com/api/v3/simple/price?ids=ethereum&vs_currencies=usd";
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);

            return json["ethereum"]["usd"].Value<decimal>();
        }

        private static BigInteger ConvertToWei(decimal ethAmount, int decimals)
        {
            decimal factor = (decimal)Math.Pow(10, decimals);
            return new BigInteger(ethAmount * factor);
        }
    }
}
