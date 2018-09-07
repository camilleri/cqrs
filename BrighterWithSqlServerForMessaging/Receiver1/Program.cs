using System;
using Paramore.Brighter;
using Paramore.Brighter.MessagingGateway.MsSql;
using Paramore.Brighter.ServiceActivator;
using Receiver1.Commands;
using Receiver1.Configuration;
using Receiver1.PipelineHandlers;
using Serilog;
using Unity;

namespace Receiver1
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
           
            var dispatcher = BuildDispatcher();
            dispatcher.Receive();

            Console.WriteLine("Press Enter to stop ...");
            Console.ReadLine();

            dispatcher.End().Wait();
        }

        private static Dispatcher BuildDispatcher()
        {
            var container = new UnityContainer();
            var handlerFactory = new UnityHandlerFactory(container);
            // var asyncHandlerFactory = new UnityAsyncHandlerFactory(container);
            var messageMapperFactory = new UnityMessageMapperFactory(container);
            container.RegisterType<IHandleRequests<GreetingEvent>, GreetingEventHandler>();

            var subscriberRegistry = new SubscriberRegistry();
            subscriberRegistry.Register<GreetingEvent, GreetingEventHandler>();
            subscriberRegistry.Register<GreetingCommand, GreetingCommandHandler>();

            //create message mappers
            var messageMapperRegistry = new MessageMapperRegistry(messageMapperFactory)
            {
                {typeof(GreetingEvent), typeof(GreetingEventMessageMapper)}
            };

            var connectionString = @"Database=BrighterSqlQueue;Server=localhost;Integrated Security=SSPI;";
            var gatewayConfig = new MsSqlMessagingGatewayConfiguration(connectionString, "QueueData");
            var messageConsumerFactory = new MsSqlMessageConsumerFactory(gatewayConfig);
            var commandProcessor = CommandProcessorBuilder.With()
                .Handlers(new HandlerConfiguration(subscriberRegistry, handlerFactory))
                .DefaultPolicy()
                .NoTaskQueues()
                .RequestContextFactory(new InMemoryRequestContextFactory())
                .Build();
            var dispatcher = DispatchBuilder.With()
                .CommandProcessor(commandProcessor)
                .MessageMappers(messageMapperRegistry)
                .DefaultChannelFactory(new MsSqlInputChannelFactory(messageConsumerFactory))
                .Connections(new Connection[]
                    {
                        new Connection<GreetingEvent>(
                            new ConnectionName("paramore.example.greeting"),
                            new ChannelName("greeting.event"),
                            new RoutingKey("greeting.event"),
                            timeoutInMilliseconds: 200)
                    }
                ).Build();

            container.RegisterInstance<IAmACommandProcessor>(commandProcessor);     
            container.RegisterType<IHandleRequests<GreetingCommand>, GreetingCommandHandler>();
            container.RegisterType<MyValidationHandler<GreetingCommand>, GreetingCommandValidationHandler>();
            container.RegisterType<MyPostAuditHandler<GreetingCommand>, GreetingCommandPostAuditHandler>();

            return dispatcher;
        }
    }
}
