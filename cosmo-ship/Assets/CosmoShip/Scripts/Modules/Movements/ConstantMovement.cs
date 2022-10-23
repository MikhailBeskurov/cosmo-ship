using CosmoShip.Scripts.Models.Movement;
using UnityEngine;

namespace CosmoShip.Scripts.Modules.Movements
{
    public class ConstantMovement : BaseMovementModule
    {
        public override Vector2 VelocityMovement => _velocityMovement;
        
        private Vector2 _velocityMovement;
        
        public ConstantMovement(IMovementData baseMovementData)
        {
            _position.Value = baseMovementData.CurrentPosition;
            _rotation.Value = baseMovementData.CurrentRotation;
            
            _directionMove = baseMovementData.DirectionMove.Value;
            _directionRotation = baseMovementData.DirectionRotation.Value;
            
            _inertiaVelocity = baseMovementData.InertiaSpeed;
            _smoothVelocity = baseMovementData.SmoothVelocity;
            
            _speedMovement = baseMovementData.SpeedMovement;
            _speedRotation = baseMovementData.SpeedRotation;
        }

        public override void Update(float deltaTime)
        {
            _velocityMovement = _directionMove;
            base.Update(deltaTime);
        }
    }
}