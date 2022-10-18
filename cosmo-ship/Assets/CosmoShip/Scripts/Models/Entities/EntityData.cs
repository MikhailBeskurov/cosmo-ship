using CosmoShip.Scripts.ClientServices.RXExtension;
using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using CosmoShip.Scripts.ScriptableObjects.Entities;
using UnityEngine;
using UnityEngine.Events;

namespace CosmoShip.Scripts.Models.Entities
{
    public class EntityData
    {
        public readonly EntityType TypeEntity;
        public readonly int Damage;
        public readonly float MoveSpeedEntity;
        public readonly float RotationSpeedEntity;
        
        public int HealtPoints => _healtPoints;
        
        public IReadOnlyReactiveProperty<Vector2> DirectionMove => _directionMove;
        public Vector2 CurrentPosition => _currentPosition;
        
        private int _healtPoints;
        private Vector2 _currentPosition;
        private ReactiveProperty<Vector2> _directionMove = new ReactiveProperty<Vector2>();
        private event UnityAction _onDestroy;
        private DisposableList _disposableList = new DisposableList();
        public EntityData(EntitiesSettingsData settingsData, Vector2 currentPosition, Vector2 directionMove)
        {
            TypeEntity = settingsData.EntityType;
            _healtPoints = settingsData.HealtsPoints;
            Damage = settingsData.Damage;
            MoveSpeedEntity = settingsData.MoveSpeedEntity;
            RotationSpeedEntity = settingsData.RotationSpeedEntity;
            _currentPosition = currentPosition;
            _directionMove.Value = directionMove;
        }

        public void OnDestroy(UnityAction onDestroy)
        {
            _onDestroy += onDestroy;
        }

        public void OnDamage(int healtPoints)
        {
            _healtPoints -= healtPoints;
            if (_healtPoints <= 0)
            {
                _onDestroy?.Invoke();
            }
        }

        public void SubscribeDirectionMove(IReadOnlyReactiveProperty<Vector2> reactiveMove)
        {
            _disposableList.Dispose();
            reactiveMove.Subscribe(v =>
            {
                _directionMove.Value = v;
            }).AddDispose(_disposableList);
        }
        
        public void SetCurrentPosition(Vector2 newPosition)
        {
            _currentPosition = newPosition;
        }
    }
}