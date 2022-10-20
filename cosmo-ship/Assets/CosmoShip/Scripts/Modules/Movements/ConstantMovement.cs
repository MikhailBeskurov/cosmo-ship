using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using UnityEngine;

namespace CosmoShip.Scripts.Modules.Movements
{
    public class ConstantMovement : IMovementModule
    {
        public IReadOnlyReactiveProperty<Vector2> Position => _position;
        public IReadOnlyReactiveProperty<Quaternion> Rotation => _rotation;
        public IReadOnlyReactiveProperty<float> InstantSpeed => _instantSpeed;
        public IReadOnlyReactiveProperty<float> AngleRotation => _angleRotation;

        private ReactiveProperty<Vector2> _position = new ReactiveProperty<Vector2>();
        private ReactiveProperty<Quaternion> _rotation = new ReactiveProperty<Quaternion>();
        private ReactiveProperty<float> _instantSpeed = new ReactiveProperty<float>();
        private ReactiveProperty<float> _angleRotation = new ReactiveProperty<float>();
        
        private Vector2 _directionMove;
        private Vector3 _directionRotation;

        private float _speedMove;
        private float _speedRotation;
        
        public ConstantMovement(Vector2 positionInit, Quaternion rotationInit, Vector2 directionMove, float speedMove,
            Vector3 directionRotation, float speedRotation)
        {
            _position.Value = positionInit;
            _rotation.Value = rotationInit;
            _directionMove = directionMove;
            _directionRotation = directionRotation;
            _speedMove = speedMove;
            _speedRotation = speedRotation;
        }

        public void Update(float deltaTime)
        {
            var pastPosition = Position.Value;
            
            _position.Value = Vector2.Lerp(Position.Value, Position.Value + _directionMove, deltaTime * _speedMove);
            _rotation.Value = Quaternion.Lerp(Rotation.Value, 
                Quaternion.Euler(Rotation.Value.eulerAngles + _directionRotation), deltaTime * _speedRotation);
            
            _instantSpeed.Value = ((Position.Value - pastPosition) / deltaTime).magnitude;
            _angleRotation.Value = Quaternion.Angle(Rotation.Value, Quaternion.Euler(Vector2.up));
        }

        public void TeleportationToPoint(Vector2 position)
        {
            _position.Value = position;
        }
    }
}