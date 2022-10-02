using System;
using CosmoShip.Scripts.Utils.RXExtension;

namespace CosmoShip.Scripts.ClientServices.RXExtension.Property
{
    public class ReactiveProperty<DataType> : IReadOnlyReactiveProperty<DataType>
    {
        public DataType Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                _onValueChanged?.Invoke(_value);
            }
        }

        private DataType _value;
        private event Action<DataType> _onValueChanged;
        
        public IReadOnlyReactiveProperty<DataType> Subscribe(Action<DataType> onAction)
        {
            _onValueChanged += onAction;
            return this;
        }
        
        public IReadOnlyReactiveProperty<DataType> AddDispose(DisposableList disposableList)
        {
            disposableList.Add(this);
            return this;
        }

        public void Dispose()
        {
            _onValueChanged = null;
        }
    }
}