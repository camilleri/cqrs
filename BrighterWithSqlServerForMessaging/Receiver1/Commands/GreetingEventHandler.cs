using System;
using Paramore.Brighter;

namespace Receiver1.Commands
{
    public class GreetingEventHandler : RequestHandler<GreetingEvent>
    {
        private readonly IAmACommandProcessor _commandProcessor;

        public GreetingEventHandler(IAmACommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        public override GreetingEvent Handle(GreetingEvent @event)
        {
            Console.WriteLine($"In the event handler for: {@event.Greeting}");

            _commandProcessor.Send(new GreetingCommand(@event.Greeting, fail: false));
            _commandProcessor.Send(new GreetingCommand(@event.Greeting, fail: true));

            return base.Handle(@event);
        }
    }
}