using MassTransit;
using MassTransit.Logging;
using OpenTelemetry;
using OpenTelemetry.OneMicroservice.Services;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient<StockService>(options => { options.BaseAddress = new Uri("https://localhost:7266"); });


builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, config) =>
    {
        config.Host(new Uri(builder.Configuration.GetConnectionString("RabbitMQ")!), host => { });
    });
});


builder.Services.AddOpenTelemetry().WithTracing(options =>
{
    options.AddSource(DiagnosticHeaders.DefaultListenerName).AddSource("One.Microservice.Activity.Source")
        .ConfigureResource(cr =>
        {
            cr.AddService("One.Microservice", serviceVersion: "1.0.0").AddAttributes(
                new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("env", builder.Environment.EnvironmentName)
                });
        });
    options.AddAspNetCoreInstrumentation(options =>
    {
        options.Filter += (httpContext) => httpContext.Request.Path.Value!.Contains("api");


        options.RecordException = true;
        options.EnrichWithHttpRequest += ((activity, request) =>
        {
            activity.SetTag("header.isHttp", request.IsHttps);
        });
    });
}).UseOtlpExporter();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();