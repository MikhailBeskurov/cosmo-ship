using System;
using CosmoShip.Scripts.Utils.RXExtension;

namespace CosmoShip.Scripts.ClientServices.RXExtension
{
    public interface IObserveReset<DataType>
    {
        public IObserveReset<DataType> Subscribe(Action onAction);
        public IObserveReset<DataType> AddDispose(DisposableList disposableList);
    }
}