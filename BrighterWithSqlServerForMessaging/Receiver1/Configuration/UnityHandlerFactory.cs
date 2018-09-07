using System;
using Paramore.Brighter;
using Unity;

namespace Receiver1.Configuration
{
    public class UnityHandlerFactory : IAmAHandlerFactory
    {
        private readonly UnityContainer _container;

        public UnityHandlerFactory(UnityContainer container)
        {
            _container = container;
        }

        public IHandleRequests Create(Type handlerType)
        {
            return (IHandleRequests)_container.Resolve(handlerType);
        }

        public void Release(IHandleRequests handler)
        {
            if (handler is IDisposable disposable)
            {
                disposable.Dispose();
            }
            handler = null;
        }
    }
}
