// modules/RateLimitModule.cs
using System;
using webAegis.api;
using System.Collections.Generic;
using modules;

public class RateLimitModule : ProtectionModule
{
    private Dictionary<string, DateTime> lastRequestPerIp = new();

    public RateLimitModule() : base("RateLimit") {}

    protected override bool PerformCheck(RequestEvent request)
    {
        if (!lastRequestPerIp.ContainsKey(request.ClientIp))
        {
            lastRequestPerIp[request.ClientIp] = request.Timestamp;
            return true;
        }

        var last = lastRequestPerIp[request.ClientIp];
        var diff = (request.Timestamp - last).TotalSeconds;

        lastRequestPerIp[request.ClientIp] = request.Timestamp;
        return diff >= 2; // например, блокируем если меньше 2 секунд между запросами
    }
}
