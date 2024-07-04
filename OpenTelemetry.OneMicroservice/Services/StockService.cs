namespace OpenTelemetry.OneMicroservice.Services
{
    public class StockService(HttpClient httpClient)
    {
        public async Task<string> GetStock()
        {
            var response = await httpClient.GetAsync("/api/Stock");

            var responseAsText = await response.Content.ReadAsStringAsync();

            return responseAsText;
        }
    }
}