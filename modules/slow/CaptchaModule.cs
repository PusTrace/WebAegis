// modules/CaptchaModule.cs
using System;
using webAegis.api;

namespace modules
{
    public class CaptchaModule : ProtectionModule
    {
        public CaptchaModule() : base("Captcha") { }

        protected override ModuleResult PerformCheck(RequestEvent request)
        {
            // Демонстрация: ставим вопрос пользователю
            Console.WriteLine($"[Captcha] Challenge for IP {request.ClientIp}. Enter code 1234 to pass:");
            var input = Console.ReadLine();
            if (input != null && input.Trim() == "1234")
            {
                return ModuleResult.Allow();
            }
            return ModuleResult.Block("Captcha failed");
        }
    }
}
