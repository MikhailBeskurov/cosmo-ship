using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using CosmoShip.Scripts.Models.Movement;
using CosmoShip.Scripts.ScriptableObjects.Entities;
using UnityEngine;
using UnityEngine.Events;

namespace CosmoShip.Scripts.Models.Entities
{
    public class EntityData : MovementData
    {
        public EntityType TypeEntity => _typeEntity;
        public int HealtPoints => _healtPoints;
        public int ScoreOnDestroy => _scoreOnDestroy;
        public int Damage => _damage;

        private EntityType _typeEntity;
        private int _healtPoints;
        private int _scoreOnDestroy;
        private int _damage;
        private event UnityAction _onDestroy;
        
        public EntityData(EntitiesSettingsData settingsData)
        {
            _typeEntity = settingsData.EntityType;
            _healtPoints = settingsData.HealtsPoints;
            _damage = settingsData.Damage;
            _scoreOnDestroy = settingsData.ScoreOnDestroy;
            InitSettingsMovement(settingsData.MovementSettings);
        }
        
        public void OnDestroy(UnityAction onDestroy)
        {
            _onDestroy += onDestroy;
        }

        public void PutDamage(int healtPoints)
        {
            _healtPoints -= healtPoints;
            if (_healtPoints <= 0)
            {
                _onDestroy?.Invoke();
            }
        }
    }
}