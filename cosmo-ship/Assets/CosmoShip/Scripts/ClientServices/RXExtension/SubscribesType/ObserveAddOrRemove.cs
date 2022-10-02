using System;
using System.Collections.Generic;
using CosmoShip.Scripts.Utils.RXExtension;
using UnityEngine;

namespace CosmoShip.Scripts.ClientServices.RXExtension
{
    public class ObserveAddOrRemove<DataType> : IObserveAddOrRemove<DataType>
    {
        private Action<DataType> sAction;
        public void OnAction(DataType value)
        {
            sAction?.Invoke(value);
        }
       
        public IObserveAddOrRemove<DataType> Subscribe(Action<DataType> onAction)
        {
            sAction = onAction;
            return this;
        }
        
        public IObserveAddOrRemove<DataType> AddDispose(DisposableList disposableList)
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