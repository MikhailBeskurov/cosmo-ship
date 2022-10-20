using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using UnityEngine;

namespace CosmoShip.Scripts.Modules.Movements
{
    public class InputMovement : IMovementModule
    {
        public IReadOnlyReactiveProperty<Vector2> Position => _position;
        public IReadOnlyReactiveProperty<Quaternion> Rotation => _rotation;
        
        private ReactiveProperty<Vector2> _position = new ReactiveProperty<Vector2>();
        private ReactiveProperty<Quaternion> _rotation = new ReactiveProperty<Quaternion>();

        private float _speedMovement;
        private float _speedRotation;
        private float _inertiaVelocity;
        private float _smoothInputSpeed = 0.5f;
        
        private Vector2 _inputMoveVector = Vector2.zero;
        private Quaternion _nextRotation = Quaternion.identity;
        
        private Vector2 _currentInputVector = Vector2.zero;
        private Vector2 _smoothInputVelocity = Vector2.zero;

        private Vector2 _currentInertiaVector = Vector2.zero;
        private Vector2 _smoothInertiaVelocity = Vector2.zero;
        
        private bool _dragg = false;
        
        public InputMovement(IReadOnlyReactiveProperty<Vector2> positionPlayer, 
            IReadOnlyReactiveProperty<Quaternion> rotationPlayer, float speedMovement, float speedRotation, float inertiaVelocity = 0)
        {
            _speedMovement = speedMovement;
            _speedRotation = speedRotation;
            _inertiaVelocity = inertiaVelocity;
            
            positionPlayer.Subscribe(v =>
            {
                if (v == Vector2.zero)
                {
                    _dragg = false;
                    _currentInputVector = Vector2.zero;
                    _smoothInputVelocity = Vector2.zero;
                }
                else
                {
                    _dragg = true;
                }
                _inputMoveVector = v;
            });
            rotationPlayer.Subscribe(v =>
            {
                _nextRotation = v;
            });
        }

        public void Update(float deltaTime)
        {  
             _rotation.Value = Quaternion.LerpUnclamped(_rotation.Value,
                 Quaternion.Euler(_rotation.Value.eulerAngles + _nextRotation.eulerAngles), deltaTime * _speedRotation);
          
            InertiaMovement(deltaTime);
        }
        
        public void TeleportationToPoint(Vector2 position)
        {
            _position.Value = position;
        }
        
        private void InertiaMovement(float deltaTime)
        {
            Quaternion rotation = Quaternion.identity;
            
            if (_dragg)
            { 
                rotation = Rotation.Value;
                _currentInputVector = Vector2.SmoothDamp(_currentInputVector, _inputMoveVector,
                    ref _smoothInputVelocity, _smoothInputSpeed);
            }
            
            var positionNextStep = (Vector2)(rotation * _currentInputVector);
            var inertiaNextStep = rotation * _currentInputVector * _inertiaVelocity;

            _currentInertiaVector = Vector2.Lerp(_currentInertiaVector, inertiaNextStep, deltaTime);
            _position.Value = Vector2.Lerp(Position.Value, Position.Value + positionNextStep + _currentInertiaVector, 
                deltaTime * _speedMovement);
        }
    }
}