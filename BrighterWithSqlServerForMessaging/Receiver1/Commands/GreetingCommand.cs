using System;
using Paramore.Brighter;

namespace Receiver1.Commands
{
    public class GreetingCommand : Command
    {
        public GreetingCommand() : base(Guid.NewGuid()) { }

        public GreetingCommand(string greeting, bool fail) : base(Guid.NewGuid())
        {
            Greeting = greeting;
            Fail = fail;
        }

        public string Greeting { get; set; }
        public bool Fail { get; set;  }
    }
}
