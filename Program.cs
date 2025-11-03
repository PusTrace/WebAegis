// Program.cs
using System;
using webAegis.api;
using modules;

class Program
{
    static void Main()
    {
        var manager = new WebAegisManager();

        // Регистрируем быстрые и медленные модули
        manager.AddFastModule(new RateLimitModule());
        manager.AddSlowModule(new CaptchaModule()); // будет вызвана при блоке RateLimit

        var broker = new RequestBroker(manager);

        Console.WriteLine("Simulating normal requests...");
        var r1 = broker.SimulateRequest("1.2.3.4", "/login");
        Console.WriteLine($"Result: Allowed={r1.Allowed} Reason='{r1.Reason}'");

        System.Threading.Thread.Sleep(500);
        var r2 = broker.SimulateRequest("1.2.3.4", "/login");
        Console.WriteLine($"Result: Allowed={r2.Allowed} Reason='{r2.Reason}'");

        Console.WriteLine("Simulating DDoS-ish burst (10 fast requests)...");
        for (int i = 0; i < 10; i++)
        {
            var res = broker.SimulateRequest("9.9.9.9", "/login");
            Console.WriteLine($"[{i}] -> Allowed={res.Allowed} Reason='{res.Reason}'");
            System.Threading.Thread.Sleep(100); // 0.1s
        }

        Console.WriteLine("Done.");
    }
}
