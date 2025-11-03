using modules;  // <- теперь ProtectionModule виден
using System;

// webAegis.api/RequestBroker.cs
using System;
using webAegis.api;

namespace webAegis.api
{
    public class RequestBroker
    {
        private WebAegisManager manager;

        public RequestBroker(WebAegisManager manager)
        {
            this.manager = manager;
        }

        // Возвращаем результат проверки
        public ModuleResult SimulateRequest(string clientIp, string path)
        {
            var request = new RequestEvent
            {
                ClientIp = clientIp,
                Path = path,
                Timestamp = DateTime.Now
            };

            var result = manager.ProcessRequest(request);
            return result;
        }
    }

    public class RequestEvent
    {
        public string ClientIp { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
