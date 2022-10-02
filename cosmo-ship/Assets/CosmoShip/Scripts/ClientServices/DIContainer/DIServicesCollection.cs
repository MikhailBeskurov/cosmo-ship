using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CosmoShip.Scripts.ClientServices.DIContainer.Attributes;
using CosmoShip.Scripts.Utils.RXExtension;
using Object = System.Object;

namespace CosmoShip.Scripts.ClientServices.DIContainer
{
    public interface IDIServicesCollection
    {
        void BindFromInstance<TService>(TService implementation);
        void BindFromNew<TService>();
        void Dispose();
    }

    public class DIServicesCollection : IDIServicesCollection
    {
        private List<Object> ServicesInstall = new List<Object>();
        
        private List<ServiceDescriptor> _unrelatedServices = new List<ServiceDescriptor>();
        private DisposableList _disposableList = new DisposableList();

        public void BindFromInstance<TService>(TService implementation)
        {
            var service = new ServiceDescriptor(implementation, ServiceLifetime.FromInstance);
            Bind(service);
        }

        public void BindFromNew<TService>()
        {
            var service = new ServiceDescriptor(typeof(TService), ServiceLifetime.FromNew);
            Bind(service);
        }
        
        private bool InjectionConstructors(ServiceDescriptor serviceDescriptor)
        {
            var isSuccessInject = false;
            var constructors = serviceDescriptor.ServiceType.GetConstructors();
            foreach (var constructorInfo in constructors)
            {
                try
                {
                    var objectsParameters = FindServices(constructorInfo.GetParameters());
                    if (objectsParameters.Length > 0)
                    {
                        serviceDescriptor.SetObject(Activator.CreateInstance(serviceDescriptor.ServiceType,
                            objectsParameters));
                    }
                    else
                    {
                        serviceDescriptor.SetObject(Activator.CreateInstance(serviceDescriptor.ServiceType));
                    }
                    isSuccessInject = true;
                    break;
                }
                catch (Exception e)
                {
                    isSuccessInject = false;
                }
            }

            return isSuccessInject;
        }
        
        private bool InjectionMethods(ServiceDescriptor serviceDescriptor)
        {
            var isSuccessInject = true;
            var methods = serviceDescriptor.ServiceObject.GetType().GetMethods()
                .ToList().FindAll(v => v.CustomAttributes
                    .ToList().Exists(a => a.AttributeType == typeof(Injection)));
            
            foreach (var methodInfo in methods)
            {
                try
                {
                    methodInfo.Invoke(serviceDescriptor.ServiceObject, 
                        FindServices(methodInfo.GetParameters()));
                }
                catch (Exception e)
                {
                    isSuccessInject = false;
                }
            }

            return isSuccessInject;
        }

        private object[] FindServices(ParameterInfo[] parametersInfo)
        {
            List<object> objectsParameters = new List<object>();
            foreach (var param in parametersInfo)
            {
                object findService;
                if (param.ParameterType.IsInterface)
                {
                    findService = ServicesInstall.Find(type => type.GetType().GetInterfaces()
                        .ToList().Exists(IType => IType == param.ParameterType));
                }
                else
                {
                    findService = ServicesInstall.Find(v => v.GetType() == param.ParameterType);
                }
                if (findService != null)
                {
                    objectsParameters.Add(findService);
                }
            }
            return objectsParameters.ToArray();
        }

        private void InitServices()
        {
            for (int i = 0; i < _unrelatedServices.Count; i++)
            {
                var service = _unrelatedServices[i];
                Bind(service);
            }
        }

        private void Bind(ServiceDescriptor serviceDescriptor)
        {
            bool success = false;
            switch (serviceDescriptor.ServiceLifetime)
            {
                case ServiceLifetime.FromNew:
                    success = InjectionConstructors(serviceDescriptor);
                    InstantiateService(serviceDescriptor, success);
                    break;
                case ServiceLifetime.FromInstance:
                    success = InjectionMethods(serviceDescriptor);
                    InstantiateService(serviceDescriptor, success);
                    break;
            }
        }

        private void InstantiateService(ServiceDescriptor serviceDescriptor, bool isSuccessInject)
        {
            var isUnrelatedServices = _unrelatedServices
                .Exists(v => v.ServiceObject == serviceDescriptor.ServiceObject);
            
            if(isSuccessInject)
            {
                ServicesInstall.Add( serviceDescriptor.ServiceObject);
                AddDisposable(serviceDescriptor);
                if (isUnrelatedServices)
                {
                    _unrelatedServices.Remove(serviceDescriptor);
                }
                InitServices();
            }
            else
            {
                if (!isUnrelatedServices)
                {
                    _unrelatedServices.Add(serviceDescriptor);
                }
            }
        }

        private void AddDisposable(ServiceDescriptor serviceDescriptor)
        {
            var isServiceDisposable = serviceDescriptor.ServiceObject.GetType().GetInterfaces()
                .ToList().Exists(IType => IType == typeof(IDisposable));
            if (isServiceDisposable)
            {
                _disposableList.Add(serviceDescriptor.ServiceObject as IDisposable);
            }
        }

        public void Dispose()
        {
            _disposableList.Dispose();
        }
    }
}