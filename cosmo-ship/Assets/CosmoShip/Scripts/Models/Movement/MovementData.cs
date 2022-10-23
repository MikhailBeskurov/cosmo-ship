using System;
using CosmoShip.Scripts.ClientServices.RXExtension;
using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace CosmoShip.Scripts.Models.Movement
{
    public interface IMovementData
    {
        public Vector2 CurrentPosition { get; }
        public Quaternion CurrentRotation { get; }
        public IReadOnlyReactiveProperty<Vector2> DirectionMove { get; }
        public IReadOnlyReactiveProperty<Quaternion> DirectionRotation { get; }

        public float SpeedRotation { get; }
        public float SpeedMovement { get; }
        public float SmoothVelocity { get; }
        public float InertiaSpeed { get; }
        
        public void PositionTo(Vector2 newPosition);
    }
    
    public abstract class MovementData : IMovementData
    {
        public Vector2 CurrentPosition => _currentPosition;
        public Quaternion CurrentRotation => _currentRotation;
        public IReadOnlyReactiveProperty<Vector2> DirectionMove => _directionMove;
        public IReadOnlyReactiveProperty<Quaternion> DirectionRotation => _directionRotation;

        public float SpeedRotation => _movementSettings.SpeedRotation;
        public float SpeedMovement => _movementSettings.SpeedMovement;
        public float SmoothVelocity => _movementSettings.SmoothVelocity;
        public float InertiaSpeed => _movementSettings.InertiaSpeed;
        
        private Vector2 _currentPosition;
        private Quaternion _currentRotation;
        
        private ReactiveProperty<Vector2> _directionMove = new ReactiveProperty<Vector2>();
        private ReactiveProperty<Quaternion> _directionRotation = new ReactiveProperty<Quaternion>();

        private MovementSettings _movementSettings;
        private DisposableList _disposableList = new DisposableList();
        
        public void InitSettingsMovement(MovementSettings movementSettings)
        {
            _movementSettings = movementSettings;
        }

        public MovementData InitMovement(Vector2 initPosition, Quaternion initRotation, 
            Vector2 directionMove, Quaternion directionRotation)
        {
            Dispose();
            _currentPosition = initPosition;
            _currentRotation = initRotation;
            _directionMove.Value = directionMove;
            _directionRotation.Value = directionRotation;
            return this;
        }
        
        public MovementData InitMovement(Vector2 initPosition, Quaternion initRotation, 
            IReadOnlyReactiveProperty<Vector2> directionMove, Quaternion directionRotation)
        {
            Dispose();
            _currentPosition = initPosition;
            _currentRotation = initRotation;
            directionMove.Subscribe(v =>
            {
                _directionMove.Value = v;

            }).AddDispose(_disposableList);
            _directionRotation.Value = directionRotation;
            return this;
        }
        
        public MovementData InitMovement(Vector2 initPosition, Quaternion initRotation, 
            IReadOnlyReactiveProperty<Vector2> directionMove, IReadOnlyReactiveProperty<Quaternion> directionRotation)
        {
            Dispose();
            _currentPosition = initPosition;
            _currentRotation = initRotation;
            directionMove.Subscribe(v =>
            {
                _directionMove.Value = v;

            }).AddDispose(_disposableList);
            directionRotation.Subscribe(v =>
            {
                _directionRotation.Value = v;
            }).AddDispose(_disposableList);
            return this;
        }
        
        public void PositionTo(Vector2 newPosition)
        {
            _currentPosition = newPosition;
        }

        private void Dispose()
        {
            _disposableList.Dispose();
        }
    }
}