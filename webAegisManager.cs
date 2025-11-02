using System;
using System.Collections.Generic;
using modules; // <- важно!
using webAegis.api; // если используешь RequestEvent

public class WebAegisManager
{
    private List<ProtectionModule> modules = new List<ProtectionModule>();

    public void AddModule(ProtectionModule module)
    {
        modules.Add(module);
    }

    public void RunAllModules(RequestEvent request)
    {
        foreach (var module in modules)
        {
            module.RunCheck(request);
        }
    }
}
