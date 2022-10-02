using System;
using CosmoShip.Scripts.Utils.RXExtension;

namespace CosmoShip.Scripts.ClientServices.RXExtension.SubscribesType
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