using System;
using System.Collections.Generic;
using CosmoShip.Scripts.Models.Entities;
using CosmoShip.Scripts.Models.Movement;
using CosmoShip.Scripts.World.Views.Entities.Entity;
using UnityEngine;

namespace CosmoShip.Scripts.ScriptableObjects.Entities
{
    [CreateAssetMenu(fileName = "EntitiesSettings", menuName = "Settings/EntitiesSettings", order = 0)]
    public class EntitiesSettings : ScriptableObject
    {
        [SerializeField] private List<EntitiesSettingsData> _entitiesSettings = new List<EntitiesSettingsData>();
        
        public EntitiesSettingsData GetEntitySettings(EntityType entityType)
        {
            return _entitiesSettings.Find(v => v.EntityType == entityType);
        }
        
        public BaseEntityView GetEntityObject(EntityType entityType)
        {
            return _entitiesSettings.Find(v => v.EntityType == entityType).EntityView;
        }
    }
    
    [Serializable]
    public class EntitiesSettingsData
    {
        public EntityType EntityType;
        public BaseEntityView EntityView;
        public int HealtsPoints = 1;
        public int ScoreOnDestroy = 0;
        public int Damage = 1;
        public MovementSettings MovementSettings;
    }
}