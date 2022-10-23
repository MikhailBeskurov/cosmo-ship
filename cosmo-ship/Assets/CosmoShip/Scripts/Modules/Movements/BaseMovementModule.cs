using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using UnityEngine;

namespace CosmoShip.Scripts.Modules.Movements
{
    public abstract class BaseMovementModule
    {
        public IReadOnlyReactiveProperty<Vector2> Position => _position;
        public IReadOnlyReactiveProperty<Quaternion> Rotation => _rotation;
        
        public abstract Vector2 VelocityMovement { get; }

        protected ReactiveProperty<Vector2> _position = new ReactiveProperty<Vector2>();
        protected ReactiveProperty<Quaternion> _rotation = new ReactiveProperty<Quaternion>();
        
        protected Vector2 _directionMove;
        protected Quaternion _directionRotation;
        private Vector2 _currentDirectionMove;
        private Vector2 _currentInertiaMove;
        
        protected float _speedMovement;
        protected float _speedRotation;
        protected float _inertiaVelocity;
        protected float _smoothVelocity;
        
        public virtual void Update(float deltaTime)
        {
            Vector2 nextPosition = SmoothMove(VelocityMovement, deltaTime) + InertiaMove(deltaTime);
            Debug.DrawRay(Position.Value, InertiaMove(deltaTime) * 10, new Color(0.04f, 0f, 1f));
            Debug.DrawRay(Position.Value, SmoothMove(VelocityMovement, deltaTime) * 10, new Color(0.13f, 1f, 0.1f));
            Rotate(Quaternion.Euler(Rotation.Value.eulerAngles + _directionRotation.eulerAngles), deltaTime);
            MoveTranslate(Position.Value + nextPosition, deltaTime);
        }
        
        private void Rotate(Quaternion rotation, float deltaTime)
        {
            _rotation.Value = Quaternion.LerpUnclamped(Rotation.Value, rotation, deltaTime * _speedRotation); 
        }
        
        private void MoveTranslate(Vector2 position, float deltaTime)
        {
            _position.Value = Vector2.Lerp(Position.Value, position, deltaTime * _speedMovement); 
        }
        
        private Vector2 InertiaMove(float deltaTime)
        {
            if (_inertiaVelocity > 0)
            {
                _currentInertiaMove = Vector2.Lerp(_currentInertiaMove,_currentDirectionMove * _inertiaVelocity, deltaTime);
            }
            else if (!_currentInertiaMove.Equals(Vector2.zero))
            {
                _currentInertiaMove = Vector2.zero;
            }
            return _currentInertiaMove;
        }
        
        private Vector2 SmoothMove(Vector2 velocity, float deltaTime)
        {
            if (_smoothVelocity > 0)
            {
                if (!velocity.Equals(Vector2.zero))
                {
                    _currentDirectionMove = Vector2.Lerp(_currentDirectionMove, velocity, _smoothVelocity * deltaTime);
                }
                else if (!_currentDirectionMove.Equals(Vector2.zero))
                {
                    _currentDirectionMove = Vector2.zero;
                }
            }
            else if (!_currentDirectionMove.Equals(velocity))
            {
                _currentDirectionMove = velocity;
            }
            
            return _currentDirectionMove;
        }
        
        public void PositionTo(Vector2 newPosition)
        {
            _position.Value = newPosition;
        }  
    }
}