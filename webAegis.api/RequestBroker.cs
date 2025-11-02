// webAegis.api/RequestBroker.cs
using System;
using modules;

namespace webAegis.api
{
    public class RequestBroker
    {
        private WebAegisManager manager;

        public RequestBroker(WebAegisManager manager)
        {
            this.manager = manager;
        }

        // Пока пример: имитация запроса с сайта
        public void SimulateRequest(string clientIp, string path)
        {
            var request = new RequestEvent
            {
                ClientIp = clientIp,
                Path = path,
                Timestamp = DateTime.Now
            };

            // Отправляем в модули для анализа
            manager.RunAllModules(request);
        }
    }

    // Универсальный формат запроса
    public class RequestEvent
    {
        public string ClientIp { get; set; }
        public string Path { get; set; }
        public DateTime Timestamp { get; set; }
        // Можно добавлять headers, cookies и т.д.
    }
}
