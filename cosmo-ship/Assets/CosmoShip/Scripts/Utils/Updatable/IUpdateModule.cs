using System;

namespace CosmoShip.Scripts.Utils.Updatable
{
    public interface IUpdateModule
    {
        public void AddAction(Action<float> onUpdate);
        public void RemoveAction(Action<float> onUpdate);
    }
}