using modules;  // <- теперь ProtectionModule виден
using System;

namespace webAegis.api
{
    public class RequestBroker
    {
        private WebAegisManager manager;

        public RequestBroker(WebAegisManager manager)
        {
            this.manager = manager;
        }

        public void SimulateRequest(string clientIp, string path)
        {
            var request = new RequestEvent
            {
                ClientIp = clientIp,
                Path = path,
                Timestamp = DateTime.Now
            };

            manager.RunAllModules(request);
        }
    }

    public class RequestEvent
    {
        public string ClientIp { get; set; }
        public string Path { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
