using System;
using webAegis.api;

class Program
{
    static void Main()
    {
        var manager = new WebAegisManager();
        manager.AddModule(new RateLimitModule());
        // manager.AddModule(new CaptchaModule());

        var broker = new RequestBroker(manager);

        // Имитируем запросы
        broker.SimulateRequest("1.2.3.4", "/login");
        broker.SimulateRequest("1.2.3.4", "/login"); // слишком быстро — блок
        broker.SimulateRequest("5.6.7.8", "/home");
    }
}
