using System.Diagnostics;

namespace OpenTelemetryTwoMicroservice
{
    internal class ActivitySourceProvider
    {
        internal static ActivitySource Source = new ActivitySource("Two.Microservice.Activity.Source", "1.00");
    }
}