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
    options.AddSource("Two.Microservice.Activity.Source").ConfigureResource(cr =>
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