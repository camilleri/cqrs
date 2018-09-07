using System;
using System.Threading;
using Paramore.Brighter;
using Paramore.Brighter.Policies.Attributes;
using Receiver1.PipelineHandlers;

namespace Receiver1.Commands
{
    public class GreetingCommandHandler : RequestHandler<GreetingCommand>
    {
        [FallbackPolicy(true, false, 1)] // https://paramore.readthedocs.io/en/latest/PolicyFallback.html
        [MyValidationHandler(2, HandlerTiming.Before)]
        [MyPostAuditHandler(3, HandlerTiming.After)]
        public override GreetingCommand Handle(GreetingCommand command)
        {
            Console.WriteLine($"Processing {command.Greeting} {command.Fail} at {DateTime.Now}");
            Thread.Sleep(1000);
            if (command.Fail)
            {
                throw new Exception("Processing failed!");
            }

            return base.Handle(command);
        }

        public override GreetingCommand Fallback(GreetingCommand command)
        {
            Console.WriteLine($"Failed to process {command.Greeting} {command.Fail} at {DateTime.Now}, log to Failed table?");

            return base.Fallback(command);
        }
    }
}
