using Paramore.Brighter;

namespace Receiver1.PipelineHandlers
{
    public class MyPostAuditHandler<TRequest> : RequestHandler<TRequest> where TRequest : class, IRequest
    {
        private static TRequest s_command;

        public MyPostAuditHandler()
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
