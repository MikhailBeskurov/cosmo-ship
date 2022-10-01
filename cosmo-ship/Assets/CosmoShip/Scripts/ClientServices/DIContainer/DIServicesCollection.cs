using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CosmoShip.Scripts.ClientServices.DIContainer.Attributes;
using CosmoShip.Scripts.Utils.RXExtension;
using Unity.VisualScripting;
using UnityEngine;
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
            InitInstanceService(service);
        }

        public void BindFromNew<TService>()
        {
            var service = new ServiceDescriptor(typeof(TService), ServiceLifetime.FromNew);
            InitFromNewService(service);
        }
        
        public void Dispose()
        {
            _disposableList.Dispose();
        }

        private void InitServices()
        {
            for (int i = 0; i < _unrelatedServices.Count; i++)
            {
                var service = _unrelatedServices[i];
                if (service.ServiceLifetime == ServiceLifetime.FromNew)
                {
                    InitFromNewService(service);
                }
                else
                {
                    InitInstanceService(service);
                }
            }
        }
        
        private void InitFromNewService(ServiceDescriptor serviceDescriptor)
        {
            var isSuccessInject = false;
            var constructors = serviceDescriptor.ServiceType.GetConstructors();
            foreach (var constructorInfo in constructors)
            {
                List<object> objectsParameters = new List<object>();
                var parameters = constructorInfo.GetParameters();
                foreach (var param in parameters)
                {
                    var findService = ServicesInstall.Find(v => v.GetType() == param.ParameterType);
                    if (findService != null)
                    {
                        objectsParameters.Add(findService);
                    }
                }
                try
                { 
                    if (objectsParameters.Count > 0)
                    {
                        serviceDescriptor.SetObject(Activator.CreateInstance(serviceDescriptor.ServiceType,
                            objectsParameters.ToArray()));
                    }
                    else
                    {
                        serviceDescriptor.SetObject(Activator.CreateInstance(serviceDescriptor.ServiceType));
                    }
                    
                    isSuccessInject = true;
                }
                catch (Exception e)
                {
                   
                }
            }
              
            InstantiateService(serviceDescriptor,isSuccessInject);
        }
        
        private void InitInstanceService(ServiceDescriptor serviceDescriptor)
        {
            var isSuccessInject = true;
            var methods = serviceDescriptor.ServiceObject.GetType().GetMethods()
                .ToList().FindAll(v => v.CustomAttributes
                    .ToList().Exists(a => a.AttributeType == typeof(Injection)));
            
            foreach (var methodInfo in methods)
            {
                List<object> objectsParameters = new List<object>();
                foreach (var param in methodInfo.GetParameters())
                {
                    var findService = ServicesInstall.Find(v => v.GetType() == param.ParameterType);
                    if (findService != null)
                    { 
                        objectsParameters.Add(findService);
                    }
                }
                try
                {
                    methodInfo.Invoke(serviceDescriptor.ServiceObject, objectsParameters.ToArray());
                }
                catch (Exception e)
                {
                    isSuccessInject = false;
                }
            }

            InstantiateService(serviceDescriptor, isSuccessInject);
        }
        
        private void InstantiateService(ServiceDescriptor serviceDescriptor, bool isSuccessInject)
        {
            var isUnrelatedServices = _unrelatedServices.Exists(v => v.ServiceObject == serviceDescriptor.ServiceObject);
            if(isSuccessInject){
                var instanceService = serviceDescriptor.ServiceObject;
                ServicesInstall.Add(instanceService);
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
    }
}