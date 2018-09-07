using System;
using System.Threading;
using Receiver1.PipelineHandlers;

namespace Receiver1.Commands
{
    public class GreetingCommandPostAuditHandler : MyPostAuditHandler<GreetingCommand>
    {
        public override GreetingCommand Handle(GreetingCommand command)
        {
            Console.WriteLine($"Auditing command {command.Greeting} {command.Fail} at {DateTime.Now}, notify others?");
            Thread.Sleep(500);

            return base.Handle(command);
        }
    }
}