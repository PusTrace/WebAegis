// webAegis.api/ModuleResult.cs
namespace webAegis.api
{
    public class ModuleResult
    {
        public bool Allowed { get; set; } = true;
        public string Reason { get; set; } = string.Empty;

        public static ModuleResult Allow() => new ModuleResult { Allowed = true };
        public static ModuleResult Block(string reason) => new ModuleResult { Allowed = false, Reason = reason };
    }
}
