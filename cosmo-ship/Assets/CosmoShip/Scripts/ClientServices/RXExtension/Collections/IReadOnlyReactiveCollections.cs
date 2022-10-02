using System;
using System.Collections.Generic;
using CosmoShip.Scripts.ClientServices.RXExtension.Collections.SubscribeTypes;

namespace CosmoShip.Scripts.ClientServices.RXExtension.Collections
{
    public interface IReadOnlyReactiveCollections<DataType>
    {
        public List<DataType> Values { get; }
        public ISubscribeAddOrRemove<DataType> ObserveAdd();
        public ISubscribeAddOrRemove<DataType> ObserveRemove();
        public ISubscribeReset<DataType> ObserveReset();
    }
}