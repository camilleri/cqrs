using System.Threading;
using Paramore.Brighter;
using Paramore.Brighter.MessagingGateway.MsSql;
using Serilog;
using Unity;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            var commandProcessor = BuildCommandProcessor();

            int i = 0;
            while (true)
            {              
                commandProcessor.Post(new GreetingEvent($"Hello, {i}"));
                Thread.Sleep(1000);
                i++;
            }
        }

        private static CommandProcessor BuildCommandProcessor()
        {
            var messageStore = new InMemoryMessageStore();

            var container = new UnityContainer();
            var messageMapperFactory = new UnityMessageMapperFactory(container);
            var messageMapperRegistry = new MessageMapperRegistry(messageMapperFactory)
            {
                {typeof(GreetingEvent), typeof(GreetingEventMessageMapper)}
            };

            var connectionString = @"Database=BrighterSqlQueue;Server=localhost;Integrated Security=SSPI;";
            var gatewayConfig = new MsSqlMessagingGatewayConfiguration(connectionString, "QueueData");
            var producer = new MsSqlMessageProducer(gatewayConfig);

            var builder = CommandProcessorBuilder.With()
                .Handlers(new HandlerConfiguration())
                .DefaultPolicy()
                .TaskQueues(new MessagingConfiguration((IAmAMessageStore<Message>) messageStore, producer,
                    messageMapperRegistry))
                .RequestContextFactory(new InMemoryRequestContextFactory());

            var commandProcessor = builder.Build();
            return commandProcessor;
        }
    }
}