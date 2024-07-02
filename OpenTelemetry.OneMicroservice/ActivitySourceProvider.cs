using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTelemetry.Console
{
    internal class ActivitySourceProvider
    {
        internal static ActivitySource Source = new ActivitySource("One.Microservice.Activity.Source", "1.00");
    }
}