using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using CosmoShip.Scripts.Models.Movement;
using UnityEngine;

namespace CosmoShip.Scripts.Modules.Movements
{
    public class PathfinderMovement : BaseMovementModule
    {
        public override Vector2 VelocityMovement => _velocityMovement;
        
        private Vector2 _velocityMovement;

        private int _raycastCount = 12;
        private float _maxSeeAhead = 7f;
        private float _angleAhead = 135f;
        
        private Vector2 _ahead;
        private Vector2 _directionMove;
        private int _hashCode;
        
        public PathfinderMovement(IMovementData baseMovementData, int hashCode)
        {
            _position.Value = baseMovementData.CurrentPosition;
            _rotation.Value = baseMovementData.CurrentRotation;

            baseMovementData.DirectionMove.Subscribe(v =>
            {
                _directionMove = (v - Position.Value).normalized;
            });
           
            _directionRotation = baseMovementData.DirectionRotation.Value;
            
            _speedMovement = baseMovementData.SpeedMovement;
            _speedRotation = baseMovementData.SpeedRotation;
            
            _inertiaVelocity = baseMovementData.InertiaSpeed;
            _smoothVelocity = baseMovementData.SmoothVelocity;
            
            _hashCode = hashCode;
        }

        public override void Update(float deltaTime)
        {
            _ahead = collisionAvoidance();
            _velocityMovement = _ahead.magnitude > 0 ? _ahead : _directionMove;
            base.Update(deltaTime);
        }

        private Vector2 collisionAvoidance()
        {
            Vector2 sumDir = Vector2.zero;
            
            for (int i = -_raycastCount/2; i < _raycastCount/2; i++)
            {
                sumDir += findMostThreateningObstacle((360/_raycastCount) * i);
            }
            return sumDir;
        }
        
        private Vector2 findMostThreateningObstacle(float angle)
        {
            var dir = GetDirection(angle);
            var hit = Physics2D.RaycastAll(Position.Value, dir, _maxSeeAhead);

            foreach (var hit2D in hit)
            {
                if (hit2D.collider != null &&  hit2D.collider.gameObject.GetHashCode() != _hashCode && 
                    hit2D.collider.CompareTag("Entity"))
                { 
                    int sign = angle < 0 ? -1 : 1;
                    return -dir + GetDirection( angle - sign * _angleAhead);
                }
            }
            return Vector2.zero;
        }

        private Vector2 GetDirection(float angle)
        {
            var a = angle * Mathf.Deg2Rad;
            return (Vector2.up * Mathf.Cos(a) + Vector2.right * Mathf.Sin(a)).normalized;
        }
    }
}