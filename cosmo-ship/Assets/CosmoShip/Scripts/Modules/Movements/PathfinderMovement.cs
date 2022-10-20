using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using UnityEngine;

namespace CosmoShip.Scripts.Modules.Movements
{
    public class PathfinderMovement : IMovementModule
    {
        public IReadOnlyReactiveProperty<Vector2> Position => _position;
        public IReadOnlyReactiveProperty<Quaternion> Rotation => _rotation;
        
        private ReactiveProperty<Vector2> _position = new ReactiveProperty<Vector2>();
        private ReactiveProperty<Quaternion> _rotation = new ReactiveProperty<Quaternion>();

        private float _movementSpeed;
        
        private int _raycastCount = 12;
        private float _maxSeeAhead = 5f;
        private float _angleAhead = 135f;
        
        private Vector2 _ahead;
        private Vector2 _directionMove;
        private int _hashCode;
        
        public PathfinderMovement(Vector2 positionInit, Quaternion rotationInit, 
            Vector2 directionMove, float movementSpeed, int hashCode)
        {
            _movementSpeed = movementSpeed;
            _directionMove = directionMove;
            _position.Value = positionInit;
            _rotation.Value = rotationInit;
            _hashCode = hashCode;
        }
        
        public PathfinderMovement(Vector2 positionInit, Quaternion rotationInit, 
            IReadOnlyReactiveProperty<Vector2> positionTarget, float movementSpeed, int hashCode)
        {
            positionTarget.Subscribe(v =>
            {
                _directionMove = (v - Position.Value).normalized;
            });
            
            _movementSpeed = movementSpeed;
            _position.Value = positionInit;
            _rotation.Value = rotationInit;
            _hashCode = hashCode;
        }
        
        public void Update(float deltaTime)
        {
            _ahead = collisionAvoidance();
            Vector2 nextStepPos = _ahead.magnitude > 0 ? _ahead : _directionMove;
            _position.Value = Vector2.Lerp(Position.Value, Position.Value  + nextStepPos,
                deltaTime * _movementSpeed);
        }
        
        public void TeleportationToPoint(Vector2 position)
        {
            _position.Value = position;
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

            // Debug.DrawRay(Position.Value, (Vector2) dir * _maxSeeAhead,
            //     new Color(1f, 0.02f, 0f));
            
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