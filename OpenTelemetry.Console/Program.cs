// See https://aka.ms/new-console-template for more information

using OpenTelemetry;
using OpenTelemetry.Console;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

Console.WriteLine("Open Telemetry");


using var traceProvider = Sdk.CreateTracerProviderBuilder()
    .AddSource("Console.App")
    .ConfigureResource(x =>
    {
        x.AddService("Email.Sender", serviceVersion: "1.0.0").AddAttributes(new List<KeyValuePair<string, object>>()
        {
            new("evn", "development"),
            new("machine.name", Environment.MachineName)
        });
    })
    .AddConsoleExporter().AddOtlpExporter().Build();


var xService = new XService();

Console.WriteLine($"Length : {await xService.MakeRequest()}");