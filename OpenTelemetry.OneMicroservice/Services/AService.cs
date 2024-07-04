namespace OpenTelemetry.OneMicroservice.Services
{
    public class AService
    {
        public int Calculate(int a, int b)
        {
            using var activity = ActivitySourceProvider.Source.StartActivity();
            Thread.Sleep(3000);

            activity!.SetTag("calculate.a.variable", a);
            activity!.SetTag("calculate.b.variable", b);
            return a + b;
        }
    }
}