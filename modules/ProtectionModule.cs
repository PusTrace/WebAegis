using System;

namespace webAegis.Modules
{
    public abstract class ProtectionModule
    {
        public string Name { get; protected set; }

        public ProtectionModule(string name)
        {
            Name = name;
        }

        public void RunCheck()
        {
            Console.WriteLine($"Running {Name}...");
            if (PerformCheck())
                Console.WriteLine($"{Name}: Passed!");
            else
                Console.WriteLine($"{Name}: Blocked!");
        }

        protected abstract bool PerformCheck();
    }
}
