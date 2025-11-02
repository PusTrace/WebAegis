// modules/ProtectionModule.cs
using webAegis.api;

namespace modules
{
    public abstract class ProtectionModule
    {
        public string Name { get; private set; }

        public ProtectionModule(string name)
        {
            Name = name;
        }

        // Публичный метод, вызываемый менеджером
        public void RunCheck(RequestEvent request)
        {
            Console.WriteLine($"Running {Name}...");
            if (!PerformCheck(request))
            {
                Console.WriteLine($"{Name}: Blocked!");
            }
            else
            {
                Console.WriteLine($"{Name}: Passed!");
            }
        }

        // Приватная логика модуля
        protected abstract bool PerformCheck(RequestEvent request);
    }
}
