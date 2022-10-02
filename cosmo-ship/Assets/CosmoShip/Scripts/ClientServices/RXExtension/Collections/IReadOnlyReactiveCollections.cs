using System;
using System.Collections.Generic;

namespace CosmoShip.Scripts.ClientServices.RXExtension.Collections
{
    public interface IReadOnlyReactiveCollections<DataType>
    {
        public List<DataType> Values { get; }
        public IObserveAddOrRemove<DataType> ObserveAdd();
        public IObserveAddOrRemove<DataType> ObserveRemove();
        public IObserveReset<DataType> ObserveReset();
    }
}