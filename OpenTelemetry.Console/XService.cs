using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace OpenTelemetry.Console
{
    internal class XService
    {
        public async Task<int> MakeRequest()
        {
            using var activity = ActivitySourceProvider.Source.StartActivity();
            var httpClient = new HttpClient();
            activity!.AddEvent(new ActivityEvent("uzak sunucuya istek başladı"));
            var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos/1");

            var responseAsText = await response.Content.ReadAsStringAsync();

            var userId = 23;
            activity!.SetTag("user.id", userId);


            activity.AddEvent(new ActivityEvent("uzak sunucudan data alımı tamamlandı."));


            var yService = new YService();

            var fullName = yService.GetFullName("ahmet", "yıldız");


            return responseAsText.Length;
        }
    }
}