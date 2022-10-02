using System;
using CosmoShip.Scripts.Utils.RXExtension;

namespace CosmoShip.Scripts.ClientServices.RXExtension.SubscribesType
{
    public interface IObserveCountChanged
    {
        public IObserveCountChanged Subscribe(Action<int> onAction);
        public IObserveCountChanged AddDispose(DisposableList disposableList);
    }
}