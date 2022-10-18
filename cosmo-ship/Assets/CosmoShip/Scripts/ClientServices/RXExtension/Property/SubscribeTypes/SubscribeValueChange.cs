using System;

namespace CosmoShip.Scripts.ClientServices.RXExtension.Property.SubscribeTypes
{
    public interface ISubscribeValueChange<DataType>
    {
        public void AddDispose(DisposableList disposableList);
    }
    
    public class SubscribeValueChange<DataType> : ISubscribeValueChange<DataType>, IDisposable
    {
        private event Action<DataType> _onValueChanged;
        private DataType _lastValue;
        
        public void OnAction(DataType value)
        {
            _lastValue = value;
            _onValueChanged?.Invoke(value);
        }
        
        public void Subscribe(Action<DataType> onAction)
        {
            if (_lastValue != null)
            {
                onAction?.Invoke(_lastValue);
            }
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