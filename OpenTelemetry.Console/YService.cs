using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTelemetry.Console
{
    internal class YService
    {
        public string GetFullName(string firstName, string LastName)
        {
            using var activity = ActivitySourceProvider.Source.StartActivity();
            return $"{firstName} {LastName}";
        }
    }
}