using webAegis.api;

namespace modules
{
    public class CaptchaModule : ProtectionModule
    {
        public CaptchaModule() : base("Captcha") { }

        // Параметр request обязателен
        protected override bool PerformCheck(RequestEvent request)
        {
            Console.WriteLine($"CAPTCHA check for IP {request.ClientIp}");
            
            // Здесь простая имитация: всегда пропускаем
            return true;

            // Можно сделать интерактивно:
            // Console.WriteLine("Введите 1234 для прохождения CAPTCHA:");
            // var input = Console.ReadLine();
            // return input == "1234";
        }
    }
}
