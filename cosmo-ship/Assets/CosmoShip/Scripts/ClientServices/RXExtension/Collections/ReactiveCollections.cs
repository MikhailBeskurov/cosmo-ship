using System;
using System.Collections.Generic;
using CosmoShip.Scripts.ClientServices.RXExtension.Collections;
using CosmoShip.Scripts.Utils.RXExtension;

namespace CosmoShip.Scripts.ClientServices.RXExtension
{
    public class ReactiveCollections<DataType> : IReadOnlyReactiveCollections<DataType>
    {
        public List<DataType> Values => values;
        private List<DataType> values = new List<DataType>();

        private List<ObserveAddOrRemove<DataType>> _addSubscribes = new List<ObserveAddOrRemove<DataType>>();
        private List<ObserveAddOrRemove<DataType>> _removeSubscribes = new List<ObserveAddOrRemove<DataType>>();
        private List<ObserveResetSubscribe<DataType>> _resetSubscribes = new List<ObserveResetSubscribe<DataType>>();
        private List<ObserveCountChanged> _countChangeSubscribes = new List<ObserveCountChanged>();
        
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
        
        public IObserveAddOrRemove<DataType> ObserveAdd()
        {
            var subscribe = new ObserveAddOrRemove<DataType>();
            _addSubscribes.Add(subscribe);
            return subscribe;
        }
        
        public IObserveAddOrRemove<DataType> ObserveRemove()
        {
            var subscribe = new ObserveAddOrRemove<DataType>();
            _removeSubscribes.Add(subscribe);
            return subscribe;
        }
        
        public IObserveReset<DataType> ObserveReset()
        {   
            var subscribe = new ObserveResetSubscribe<DataType>();
            _resetSubscribes.Add(subscribe);
            return subscribe;
        }
        
        public IObserveCountChanged ObserveCountChange()
        {   
            var subscribe = new ObserveCountChanged();
            _countChangeSubscribes.Add(subscribe);
            return subscribe;
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