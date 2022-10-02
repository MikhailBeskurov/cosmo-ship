using System;
using CosmoShip.Scripts.ClientServices.RXExtension.Property.SubscribeTypes;
using CosmoShip.Scripts.Utils.RXExtension;

namespace CosmoShip.Scripts.ClientServices.RXExtension.Property
{
    public interface IReadOnlyReactiveProperty<DataType>
    {
        public DataType Value { get; }
        public ISubscribeValueChange<DataType> Subscribe(Action<DataType> onAction);
    }
}