using System;
using System.Collections.Generic;
using CosmoShip.Scripts.Utils.RXExtension;
using UnityEngine;

namespace CosmoShip.Scripts.ClientServices.RXExtension
{
    public class ObserveResetSubscribe<DataType> : IObserveReset<DataType>, IDisposable
    {
        private Action sAction;
        
        public void OnAction()
        {
            sAction?.Invoke();
        }

        public IObserveReset<DataType> Subscribe(Action onAction)
        {
            sAction = onAction;
            return this;
        }

        public IObserveReset<DataType> AddDispose(DisposableList disposableList)
        {
            disposableList.Add(this);
            return this;
        }

        public void Dispose()
        {
            sAction = null;
        }
    }
}