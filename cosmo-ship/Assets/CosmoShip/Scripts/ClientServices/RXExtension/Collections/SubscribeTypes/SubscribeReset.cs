using System;
using CosmoShip.Scripts.Utils.RXExtension;

namespace CosmoShip.Scripts.ClientServices.RXExtension.Collections.SubscribeTypes
{
    public interface ISubscribeReset<DataType>
    {
        public ISubscribeReset<DataType> Subscribe(Action onAction);
        public ISubscribeReset<DataType> AddDispose(DisposableList disposableList);
    }
    public class SubscribeReset<DataType> : ISubscribeReset<DataType>, IDisposable
    {
        private Action sAction;
        
        public void OnAction()
        {
            sAction?.Invoke();
        }

        public ISubscribeReset<DataType> Subscribe(Action onAction)
        {
            sAction = onAction;
            return this;
        }

        public ISubscribeReset<DataType> AddDispose(DisposableList disposableList)
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