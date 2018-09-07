using System;
using System.Threading;
using Receiver1.PipelineHandlers;

namespace Receiver1.Commands
{
    public class GreetingCommandValidationHandler : MyValidationHandler<GreetingCommand>
    {
        public override GreetingCommand Handle(GreetingCommand command)
        {
            Console.WriteLine($"Validating {command.Greeting} {command.Fail} at {DateTime.Now}");
            Thread.Sleep(500);

            if (command.Fail)
            {
                throw new Exception("Validation failed!");
            }

            return base.Handle(command);
        }
    }
}
