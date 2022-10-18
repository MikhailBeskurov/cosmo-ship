using System;
using UnityEngine;

namespace CosmoShip.Scripts.ClientServices.DIContainer
{
    public class MonoInstaller : MonoBehaviour
    {
        protected IDIServicesCollection DiContainer;

        private void Awake()
        {
            DiContainer = new DIServicesCollection();
            InstallBindings();
        }

        protected virtual void InstallBindings()
        {
            
        }

        private void OnApplicationQuit()
        {
            DiContainer.Dispose();
        }

        private void OnDisable()
        {
            DiContainer.Dispose();
        }
    }
}