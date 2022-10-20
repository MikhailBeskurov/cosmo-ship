using System;
using System.Collections.Generic;
using CosmoShip.Scripts.ClientServices.RXExtension.Property.SubscribeTypes;
using UnityEngine;

namespace CosmoShip.Scripts.ClientServices.RXExtension.Property
{
    public class ReactiveProperty<DataType> : IReadOnlyReactiveProperty<DataType>
    {
        private List<SubscribeValueChange<DataType>> _subscribeValueChange = new List<SubscribeValueChange<DataType>>();
        private DataType _lastValue;
        
        public DataType Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                ValueChange(value);
            }
        }

        private DataType _value;

        public ISubscribeValueChange<DataType> Subscribe(Action<DataType> onAction)
        {
            var subscribeType = new SubscribeValueChange<DataType>();
            subscribeType.SubscribeAction(onAction);
            
            if (_lastValue != null)
            {
                subscribeType.OnAction(_lastValue);
            }

            _subscribeValueChange.Add(subscribeType);
            return subscribeType;
        }

        private void ValueChange(DataType value)
        {
            _lastValue = value;
            foreach (var subscribe in _subscribeValueChange)
            {
                subscribe.OnAction(value);
            }
        }
    }
}