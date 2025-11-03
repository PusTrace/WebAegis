// webAegisManager.cs
using System;
using System.Collections.Generic;
using webAegis.api;
using modules;

public class WebAegisManager
{
    private readonly List<ProtectionModule> fastModules = new List<ProtectionModule>();
    private readonly List<ProtectionModule> slowModules = new List<ProtectionModule>();

    // Добавляем быстрый модуль (первым вызывается)
    public void AddFastModule(ProtectionModule module) => fastModules.Add(module);

    // Добавляем медленный модуль (капча, поведенка)
    public void AddSlowModule(ProtectionModule module) => slowModules.Add(module);

    // Основной метод: сначала запускаем fast, если one of fast blocks -> запускаем slow и возвращаем итог
    public ModuleResult ProcessRequest(RequestEvent request)
    {
        // 1) Быстрые проверки
        foreach (var m in fastModules)
        {
            var r = m.RunCheck(request);
            Console.WriteLine($"[Manager] Fast module {m.Name} => Allowed={r.Allowed} Reason='{r.Reason}'");
            if (!r.Allowed)
            {
                // Решение: при блоке быстрых модулей — запускаем медленные модули (например, капчу)
                Console.WriteLine($"[Manager] Fast module {m.Name} заблокировал. Запускаем slow modules (challenge).");
                // Запускаем slow модули один за другим — если любой пропускает, считаем allow? Обычно капча должна пройти.
                foreach (var slow in slowModules)
                {
                    var sr = slow.RunCheck(request);
                    Console.WriteLine($"[Manager] Slow module {slow.Name} => Allowed={sr.Allowed} Reason='{sr.Reason}'");
                    if (!sr.Allowed)
                    {
                        // Если кто-то из slow не прошёл — окончательный блок
                        return ModuleResult.Block($"Blocked by {slow.Name}: {sr.Reason}");
                    }
                }
                // Все slow прошли — allow (например, пользователь прошёл капчу)
                return ModuleResult.Allow();
            }
        }

        // 2) Если fast ничего не заблокировал — allow
        return ModuleResult.Allow();
    }
}
