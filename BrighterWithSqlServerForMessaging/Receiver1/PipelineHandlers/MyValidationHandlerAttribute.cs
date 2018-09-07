using System;
using Paramore.Brighter;

namespace Receiver1.PipelineHandlers
{
    public class MyValidationHandlerAttribute : RequestHandlerAttribute
    {
        public MyValidationHandlerAttribute(int step, HandlerTiming timing)
            : base(step, timing)
        {
        }

        public override Type GetHandlerType()
        {
            return typeof(MyValidationHandler<>);
        }
    }
}
