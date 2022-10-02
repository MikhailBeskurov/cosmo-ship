using System;
using CosmoShip.Scripts.Utils.RXExtension;

namespace CosmoShip.Scripts.ClientServices.RXExtension.SubscribesType
{
    public interface IObserveAddOrRemove<DataType> : IDisposable
    {
        public IObserveAddOrRemove<DataType> Subscribe(Action<DataType> onAction);
        public IObserveAddOrRemove<DataType> AddDispose(DisposableList disposableList);
        public void Dispose();
    }
}