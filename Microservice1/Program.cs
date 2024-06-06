using Microservice1.Services;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Microservice1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddHttpClient<MicroService2Service>(x =>
            {
                x.BaseAddress = new Uri(builder.Configuration.GetSection("Services")["MicroService2BaseUrl"]!);
            });


            builder.Services
                .AddRefitClient<IMicroService2Refit>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri(builder.Configuration.GetSection("Services")["MicroService2BaseUrl"]!));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
            }

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}