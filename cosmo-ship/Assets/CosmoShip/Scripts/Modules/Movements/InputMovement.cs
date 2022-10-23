using CosmoShip.Scripts.Models.Movement;
using UnityEngine;

namespace CosmoShip.Scripts.Modules.Movements
{
    public class InputMovement : BaseMovementModule
    {
        public override Vector2 VelocityMovement => _velocityMovement;
        
        private Vector2 _velocityMovement;
        
        
        public InputMovement(IMovementData baseMovementData)
        {
            _position.Value = baseMovementData.CurrentPosition;
            _rotation.Value = baseMovementData.CurrentRotation;
            
            _speedMovement = baseMovementData.SpeedMovement;
            _speedRotation = baseMovementData.SpeedRotation;
            
            _inertiaVelocity = baseMovementData.InertiaSpeed;
            _smoothVelocity = baseMovementData.SmoothVelocity;
            
            baseMovementData.DirectionMove.Subscribe(v =>
            {
                _directionMove = v;
            });
            
            baseMovementData.DirectionRotation.Subscribe(v =>
            {
                _directionRotation = v;
            });
        }

        public override void Update(float deltaTime)
        {   
            _velocityMovement = Rotation.Value * _directionMove;
            base.Update(deltaTime);
        }
    }
}