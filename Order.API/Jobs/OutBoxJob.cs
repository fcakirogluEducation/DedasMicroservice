using Microsoft.EntityFrameworkCore;
using Order.API.Models;

namespace Order.API.Jobs
{
    public class OutBoxJob(IServiceScope serviceScope) : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = serviceScope.ServiceProvider.CreateScope();
                var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var outboxList = appDbContext.OutBoxes.Where(x => x.SendBus == false).Take(100).ToList();

                foreach (var outBox in outboxList)
                {
                    //send to bus

                    outBox.SendBus = true;
                }

                appDbContext.SaveChanges();
            }


            throw new NotImplementedException();
        }
    }
}