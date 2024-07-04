using System.Diagnostics;

namespace OpenTelemetry.OneMicroservice
{
    internal class ActivitySourceProvider
    {
        internal static ActivitySource Source = new ActivitySource("One.Microservice.Activity.Source", "1.00");
    }
}