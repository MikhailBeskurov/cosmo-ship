using System;

namespace CosmoShip.Scripts.ClientServices.RXExtension.Collections.SubscribeTypes
{ 
    public interface ISubscribeCountChange
    {
        public ISubscribeCountChange Subscribe(Action<int> onAction);
        public ISubscribeCountChange AddDispose(DisposableList disposableList);
    }
    public class SubscribeCountChange: ISubscribeCountChange, IDisposable
    {
        private Action<int> sAction;
        
        public void OnAction(int count)
        {
            sAction?.Invoke(count);
        }
        
        public ISubscribeCountChange Subscribe(Action<int> onAction)
        {
            sAction = onAction;
            return this;
        }

        public ISubscribeCountChange AddDispose(DisposableList disposableList)
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