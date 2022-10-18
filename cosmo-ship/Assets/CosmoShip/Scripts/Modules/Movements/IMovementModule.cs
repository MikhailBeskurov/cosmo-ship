using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using UnityEngine;

namespace CosmoShip.Scripts.Modules.Movements
{
    public interface IMovementModule
    {
        public IReadOnlyReactiveProperty<Vector2> Position { get; }
        public IReadOnlyReactiveProperty<Quaternion> Rotation { get; }
        
        public void Update(float deltaTime);
    }
}