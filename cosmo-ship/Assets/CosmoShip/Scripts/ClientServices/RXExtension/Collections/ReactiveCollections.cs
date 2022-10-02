using System.Collections.Generic;
using CosmoShip.Scripts.ClientServices.RXExtension.Collections.SubscribeTypes;

namespace CosmoShip.Scripts.ClientServices.RXExtension.Collections
{
    public class ReactiveCollections<DataType> : IReadOnlyReactiveCollections<DataType>
    {
        public List<DataType> Values => values;
        private List<DataType> values = new List<DataType>();

        private List<SubscribeAddOrRemove<DataType>> _addSubscribes = new List<SubscribeAddOrRemove<DataType>>();
        private List<SubscribeAddOrRemove<DataType>> _removeSubscribes = new List<SubscribeAddOrRemove<DataType>>();
        private List<SubscribeReset<DataType>> _resetSubscribes = new List<SubscribeReset<DataType>>();
        private List<SubscribeCountChange> _countChangeSubscribes = new List<SubscribeCountChange>();
        
        public ISubscribeAddOrRemove<DataType> ObserveAdd()
        {
            var subscribe = new SubscribeAddOrRemove<DataType>();
            _addSubscribes.Add(subscribe);
            return subscribe;
        }
        
        public ISubscribeAddOrRemove<DataType> ObserveRemove()
        {
            var subscribe = new SubscribeAddOrRemove<DataType>();
            _removeSubscribes.Add(subscribe);
            return subscribe;
        }
        
        public ISubscribeReset<DataType> ObserveReset()
        {   
            var subscribe = new SubscribeReset<DataType>();
            _resetSubscribes.Add(subscribe);
            return subscribe;
        }
        
        public ISubscribeCountChange ObserveCountChange()
        {   
            var subscribe = new SubscribeCountChange();
            _countChangeSubscribes.Add(subscribe);
            return subscribe;
        }
        
        public void Add(DataType value)
        {
            values.Add(value);
            foreach (var subscribe in _addSubscribes)
            {
                subscribe.OnAction(value);
            }

            CollectionsCountChanged(values.Count);
        }
        
        public void Remove(DataType value)
        {
            values.Remove(value);
            foreach (var subscribe in _removeSubscribes)
            {
                subscribe.OnAction(value);
            }
            
            CollectionsCountChanged(values.Count);
        }

        public void Clear()
        {
            values.Clear();
            foreach (var subscribe in _resetSubscribes)
            {
                subscribe.OnAction();
            }

            CollectionsCountChanged(values.Count);
        }
        
        private void CollectionsCountChanged(int count)
        {
            foreach (var subscribe in _countChangeSubscribes)
            {
                subscribe.OnAction(count);
            }
        }
    }
}