// modules/RateLimitModule.cs
using System;
using System.Collections.Concurrent;
using webAegis.api;

namespace modules
{
    public class RateLimitModule : ProtectionModule
    {
        // in-memory store: ip -> (lastTimestamp, countWithinWindow)
        private static ConcurrentDictionary<string, (DateTime windowStart, int count)> store = new();
        private readonly int windowSeconds = 10;
        private readonly int maxRequests = 5;

        public RateLimitModule() : base("RateLimit") { }

        protected override ModuleResult PerformCheck(RequestEvent request)
        {
            var now = DateTime.UtcNow;
            var ip = string.IsNullOrEmpty(request.ClientIp) ? "unknown" : request.ClientIp;

            store.AddOrUpdate(ip,
                addValueFactory: (_) => (now, 1),
                updateValueFactory: (_, old) =>
                {
                    var diff = (now - old.windowStart).TotalSeconds;
                    if (diff > windowSeconds)
                        return (now, 1);
                    return (old.windowStart, old.count + 1);
                });

            var st = store[ip];
            if (st.count > maxRequests)
            {
                return ModuleResult.Block($"Too many requests: {st.count} in {windowSeconds}s");
            }

            return ModuleResult.Allow();
        }
    }
}
