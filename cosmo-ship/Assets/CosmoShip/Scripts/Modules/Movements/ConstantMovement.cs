using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using UnityEngine;

namespace CosmoShip.Scripts.Modules.Movements
{
    public class ConstantMovement : IMovementModule
    {
        public IReadOnlyReactiveProperty<Vector2> Position => _position;
        public IReadOnlyReactiveProperty<Quaternion> Rotation => _rotation;
        
        private ReactiveProperty<Vector2> _position = new ReactiveProperty<Vector2>();
        private ReactiveProperty<Quaternion> _rotation = new ReactiveProperty<Quaternion>();

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
            _position.Value = Vector2.Lerp(_position.Value, _position.Value + _directionMove, deltaTime * _speedMove);
            _rotation.Value = Quaternion.Lerp(_rotation.Value, 
                Quaternion.Euler(_rotation.Value.eulerAngles + _directionRotation), deltaTime * _speedRotation);
        }

        public void TeleportationToPoint(Vector2 position)
        {
            _position.Value = position;
        }
    }
}