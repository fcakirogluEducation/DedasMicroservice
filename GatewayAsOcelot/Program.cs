using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    JwtBearerDefaults.AuthenticationScheme, opts =>
    {
        opts.Authority = "https://localhost:5001";
        opts.Audience = "microservice.gateway";
        opts.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateLifetime = true
        };
    });


builder.Configuration.AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json");

builder.Services.AddOcelot();
var app = builder.Build();
app.Use(async (context, next) =>
{
    var loggerFactory = context.RequestServices.GetRequiredService<ILoggerFactory>();


    var logger = loggerFactory.CreateLogger("Gateway Request-Response");

    logger.LogInformation($"Request");
    await next();
    logger.LogInformation($"Response");
});
app.UseAuthentication();
app.UseAuthorization();


await app.UseOcelot();
app.Run();