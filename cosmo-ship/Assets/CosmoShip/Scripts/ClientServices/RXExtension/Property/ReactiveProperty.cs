using System;
using CosmoShip.Scripts.ClientServices.RXExtension.Property.SubscribeTypes;

namespace CosmoShip.Scripts.ClientServices.RXExtension.Property
{
    public class ReactiveProperty<DataType> : IReadOnlyReactiveProperty<DataType>
    {
        private SubscribeValueChange<DataType> _subscribeValueChange = new SubscribeValueChange<DataType>();
        
        public DataType Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                _subscribeValueChange.OnAction(_value);
            }
        }

        private DataType _value;

        public ISubscribeValueChange<DataType> Subscribe(Action<DataType> onAction)
        {
            _subscribeValueChange.Subscribe(onAction);
            return _subscribeValueChange;
        }
    }
}