using System;
using Paramore.Brighter;
using Unity;

namespace Receiver1.Configuration
{
    class UnityAsyncHandlerFactory : IAmAHandlerFactoryAsync
    {
        private readonly UnityContainer _container;

        public UnityAsyncHandlerFactory(UnityContainer container)
        {
            _container = container;
        }

        IHandleRequestsAsync IAmAHandlerFactoryAsync.Create(Type handlerType)
        {
            return (IHandleRequestsAsync)_container.Resolve(handlerType);
        }

        public void Release(IHandleRequestsAsync handler)
        {
        }
    }
}
