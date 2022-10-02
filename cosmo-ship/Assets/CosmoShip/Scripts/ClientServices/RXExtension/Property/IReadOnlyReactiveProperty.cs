using System;
using CosmoShip.Scripts.Utils.RXExtension;

namespace CosmoShip.Scripts.ClientServices.RXExtension.Property
{
    public interface IReadOnlyReactiveProperty<DataType> : IDisposable
    {
        public IReadOnlyReactiveProperty<DataType> Subscribe(Action<DataType> onAction);
        public IReadOnlyReactiveProperty<DataType> AddDispose(DisposableList disposableList);
        public void Dispose();
    }
}