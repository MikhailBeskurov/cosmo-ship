using System;
using CosmoShip.Scripts.Utils.RXExtension;

namespace CosmoShip.Scripts.ClientServices.RXExtension.SubscribesType
{
    public class ObserveCountChanged: IObserveCountChanged, IDisposable
    {
        private Action<int> sAction;
        
        public void OnAction(int count)
        {
            sAction?.Invoke(count);
        }
        
        public IObserveCountChanged Subscribe(Action<int> onAction)
        {
            sAction = onAction;
            return this;
        }

        public IObserveCountChanged AddDispose(DisposableList disposableList)
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