using System;
using Paramore.Brighter;

namespace Receiver1.Commands
{
    public class GreetingEvent : Event
    {
        public GreetingEvent() : base(Guid.NewGuid()) { }

        public GreetingEvent(string greeting) : base(Guid.NewGuid())
        {
            Greeting = greeting;
        }

        public string Greeting { get; set; }
    }
}
