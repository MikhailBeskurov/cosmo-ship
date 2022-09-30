using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq.Expressions;
using CosmoShip.Scripts.UI.Views;
using CosmoShip.Scripts.Utils.RXExtension;
using Mono.Cecil;
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
        private List<Object> _serviceDescriptors = new List<Object>();
        private DisposableList _disposableList = new DisposableList();
        private List<Type> _unrelatedServices = new List<Type>();

        public void BindFromInstance<TService>(TService implementation)
        {
           
        }

        public void BindFromNew<TService>()
        {
            InitService(typeof(TService));
        }
        
        public void Dispose()
        {
            Debug.Log("Services Dispose");
            _disposableList.Dispose();
        }

        private void InitServices()
        {
            for (int i = 0; i < _unrelatedServices.Count; i++)
            {
                InitService(_unrelatedServices[i]);
            }
        }
        
        private void InitService(Type service)
        {
            var constrctors = service.GetConstructors();
            List<object> objectsParameters = new List<object>();
            Debug.Log($"Start Init TService: {service.Name}");

            foreach (var constructorInfo in constrctors)
            {
                var parameters = constructorInfo.GetParameters();
                foreach (var parameter in parameters)
                {
                    var serviceDescriptor = _serviceDescriptors.Find(v => v.GetType() == parameter.ParameterType);
                    if (serviceDescriptor != null)
                    {
                        objectsParameters.Add(serviceDescriptor);
                    }
                }
            }

            InstantiateService(service, objectsParameters.ToArray());
        }

        private void InstantiateService(Type service, object[] objectsParameters)
        {
            try
            {
                var instanceService = Activator.CreateInstance(service, objectsParameters);
                _serviceDescriptors.Add(instanceService);
                
                if (_unrelatedServices.Exists(v => v.GetType() == service))
                {
                    _unrelatedServices.Remove(service);
                }

                InitServices();
            }
            catch (Exception e)
            {
                _unrelatedServices.Add(service);
            }
        }
    }
}