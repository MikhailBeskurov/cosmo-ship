using System;

namespace CosmoShip.Scripts.ClientServices.DIContainer
{
    public class ServiceDescriptor
    {
        public Type ServiceType { get; }
        public Object ServiceObject { get; private set; }
        public ServiceLifetime ServiceLifetime { get; }

        public ServiceDescriptor(Object serviceObject, ServiceLifetime serviceLifetime)
        {
            ServiceObject = serviceObject;
            ServiceLifetime = serviceLifetime;
        }
        
        public ServiceDescriptor(Type serviceType, ServiceLifetime serviceLifetime)
        {
            ServiceType = serviceType;
            ServiceLifetime = serviceLifetime;
        }

        public void SetObject(Object serviceObject)
        {
            ServiceObject = serviceObject;
        }
    }
}