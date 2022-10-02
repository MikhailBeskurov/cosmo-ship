using System;
using CosmoShip.Scripts.Utils.RXExtension;

namespace CosmoShip.Scripts.ClientServices.RXExtension.SubscribesType
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