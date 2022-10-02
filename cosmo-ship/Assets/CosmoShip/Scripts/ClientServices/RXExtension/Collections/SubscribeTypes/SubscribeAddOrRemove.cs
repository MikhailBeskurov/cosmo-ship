using System;
using CosmoShip.Scripts.Utils.RXExtension;

namespace CosmoShip.Scripts.ClientServices.RXExtension.Collections.SubscribeTypes
{
    public interface ISubscribeAddOrRemove<DataType> : IDisposable
    {
        public ISubscribeAddOrRemove<DataType> Subscribe(Action<DataType> onAction);
        public ISubscribeAddOrRemove<DataType> AddDispose(DisposableList disposableList);
        public void Dispose();
    }
    public class SubscribeAddOrRemove<DataType> : ISubscribeAddOrRemove<DataType>
    {
        private Action<DataType> sAction;
        public void OnAction(DataType value)
        {
            sAction?.Invoke(value);
        }
       
        public ISubscribeAddOrRemove<DataType> Subscribe(Action<DataType> onAction)
        {
            sAction = onAction;
            return this;
        }
        
        public ISubscribeAddOrRemove<DataType> AddDispose(DisposableList disposableList)
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