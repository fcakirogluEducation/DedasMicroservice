using Bus.Shared;
using MassTransit;
using MassTransit.Logging;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetryTwoMicroservice.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedEventConsumer>();
    x.UsingRabbitMq((context, config) =>
    {
        config.Host(new Uri(builder.Configuration.GetConnectionString("RabbitMQ")!), host => { });


        config.ReceiveEndpoint("two.microservice.order-created-event",
            e => { e.ConfigureConsumer<OrderCreatedEventConsumer>(context); });
    });
});

builder.Services.AddOpenTelemetry().WithTracing(options =>
{
    options.AddSource(DiagnosticHeaders.DefaultListenerName).AddSource("Two.Microservice.Activity.Source")
        .ConfigureResource(cr =>
        {
            cr.AddService("Two.Microservice", serviceVersion: "1.0.0").AddAttributes(
                new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("env", builder.Environment.EnvironmentName)
                });
        });
    options.AddAspNetCoreInstrumentation(options =>
    {
        options.Filter += (httpContext) => httpContext.Request.Path.Value!.Contains("api");


        options.RecordException = true;
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