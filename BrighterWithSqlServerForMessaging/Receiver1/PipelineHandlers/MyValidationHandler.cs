using Paramore.Brighter;

namespace Receiver1.PipelineHandlers
{
    public class MyValidationHandler<TRequest> : RequestHandler<TRequest> where TRequest : class, IRequest
    {
        private static TRequest s_command;

        public MyValidationHandler()
        {
            s_command = null;
        }

        public override TRequest Handle(TRequest command)
        {
            LogCommand(command);
            return base.Handle(command);
        }

        public static bool ShouldReceive(TRequest expectedCommand)
        {
            return (s_command != null);
        }

        private void LogCommand(TRequest request)
        {
            s_command = request;
        }
    }
}
