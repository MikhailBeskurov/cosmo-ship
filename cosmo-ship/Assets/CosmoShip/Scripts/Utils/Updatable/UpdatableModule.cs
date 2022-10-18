using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CosmoShip.Scripts.Utils.Updatable
{
    public class UpdatableModule : MonoBehaviour, IUpdateModule
    {
        private Queue<Action<float>> _queue = new Queue<Action<float>>();
        public void Update()
        {
            foreach (var action in _queue)
            {
                action?.Invoke(Time.deltaTime);
            }
        }

        public void AddAction(Action<float> onUpdate)
        {
            _queue.Enqueue(onUpdate);
        }
        
        public void RemoveAction(Action<float> onUpdate)
        {
            _queue = new Queue<Action<float>>(_queue.Where(v => v != onUpdate));
        }
    }
}