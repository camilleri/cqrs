using System;
using Paramore.Brighter;

namespace Receiver1.PipelineHandlers
{
    public class MyPostAuditHandlerAttribute : RequestHandlerAttribute
    {
        public MyPostAuditHandlerAttribute(int step, HandlerTiming timing)
            : base(step, timing)
        {
        }

        public override Type GetHandlerType()
        {
            return typeof(MyPostAuditHandler<>);
        }
    }
}
