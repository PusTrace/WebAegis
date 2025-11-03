// modules/ProtectionModule.cs
using System;
using webAegis.api;

namespace modules
{
    public abstract class ProtectionModule
    {
        public string Name { get; private set; }

        protected ProtectionModule(string name)
        {
            Name = name;
        }

        // Публичный обёрточный метод — вызывает защищённый PerformCheck и может логировать
        public ModuleResult RunCheck(RequestEvent request)
        {
            try
            {
                return PerformCheck(request) ?? ModuleResult.Allow();
            }
            catch (Exception ex)
            {
                // В модуле что-то упало — по умолчанию пропускаем, но логируем причину
                Console.WriteLine($"[{Name}] exception: {ex.Message}");
                return ModuleResult.Allow();
            }
        }

        // Наследники реализуют логику и возвращают ModuleResult
        protected abstract ModuleResult PerformCheck(RequestEvent request);
    }
}
