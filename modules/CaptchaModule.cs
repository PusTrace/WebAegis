using System;

namespace webAegis.Modules
{
    public class CaptchaModule : ProtectionModule
    {
        public CaptchaModule() : base("CAPTCHA Module") { }

        protected override bool PerformCheck()
        {
            return AskCaptcha();
        }

        private bool AskCaptcha()
        {
            Console.WriteLine("Введите CAPTCHA: 1234");
            string input = Console.ReadLine();
            return input == "1234";
        }
    }
}
