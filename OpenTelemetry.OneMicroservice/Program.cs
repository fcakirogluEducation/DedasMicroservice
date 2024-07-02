using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenTelemetry().WithTracing(options =>
{
    options.AddSource("One.Microservice.Activity.Source").ConfigureResource(cr =>
    {
        cr.AddService("One.Microservice", serviceVersion: "1.0.0").AddAttributes(
            new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("env", builder.Environment.EnvironmentName)
            });
    });
    options.AddAspNetCoreInstrumentation();
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