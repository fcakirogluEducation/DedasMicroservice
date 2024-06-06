using System.Formats.Asn1;
using System.Text.Json;

namespace Microservice1.Services
{
    public class MicroService2Service(HttpClient client)
    {
        public Task<List<string>?> GetCityList()
        {
            return client.GetFromJsonAsync<List<string>>("/api/Values");


            //var result = await client.GetAsync("/api/Values");


            //if (!result.IsSuccessStatusCode) return null;


            //return await result.Content.ReadFromJsonAsync<List<string>>();


            //var content = await result.Content.ReadAsStringAsync();
            //return JsonSerializer.Deserialize<List<string>>(content)!;
        }
    }
}