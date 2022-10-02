using System;
using CosmoShip.Scripts.Utils.RXExtension;

namespace CosmoShip.Scripts.ClientServices.RXExtension.Property.SubscribeTypes
{
    public interface ISubscribeValueChange<DataType>
    {
        public void AddDispose(DisposableList disposableList);
    }
    
    public class SubscribeValueChange<DataType> : ISubscribeValueChange<DataType>, IDisposable
    {
        private event Action<DataType> _onValueChanged;

        public void OnAction(DataType value)
        {
            _onValueChanged?.Invoke(value);
        }
        
        public void Subscribe(Action<DataType> onAction)
        {
            _onValueChanged += onAction;
        }
        
        public void AddDispose(DisposableList disposableList)
        {
            disposableList.Add(this);
        }

        public void Dispose()
        {
            _onValueChanged = null;
        }
    }
}