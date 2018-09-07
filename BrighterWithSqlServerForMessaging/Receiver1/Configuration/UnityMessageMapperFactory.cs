using System;
using Paramore.Brighter;
using Unity;

namespace Receiver1.Configuration
{
    public class UnityMessageMapperFactory : IAmAMessageMapperFactory
    {
        private readonly UnityContainer _container;

        public UnityMessageMapperFactory(UnityContainer container)
        {
            _container = container;
        }

        public IAmAMessageMapper Create(Type messageMapperType)
        {
            return (IAmAMessageMapper)_container.Resolve(messageMapperType);
        }
    }
}
