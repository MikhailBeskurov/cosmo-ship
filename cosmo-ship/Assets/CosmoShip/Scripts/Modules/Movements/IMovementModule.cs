using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using UnityEngine;

namespace CosmoShip.Scripts.Modules.Movements
{
    public interface IMovementModule
    {
        public IReadOnlyReactiveProperty<Vector2> Position { get; }
        public IReadOnlyReactiveProperty<Quaternion> Rotation { get; }
        public IReadOnlyReactiveProperty<float> InstantSpeed { get; }
        public IReadOnlyReactiveProperty<float> AngleRotation { get; }
        
        public void Update(float deltaTime);
        public void TeleportationToPoint(Vector2 position);
    }
}