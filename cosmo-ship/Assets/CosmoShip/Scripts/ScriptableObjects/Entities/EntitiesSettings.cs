using System;
using System.Collections.Generic;
using CosmoShip.Scripts.Models.Entities;
using UnityEngine;

namespace CosmoShip.Scripts.ScriptableObjects.Entities
{
    [CreateAssetMenu(fileName = "EntitiesSettings", menuName = "Settings/EntitiesSettings", order = 0)]
    public class EntitiesSettings : ScriptableObject
    {
        [SerializeField] private List<EntitiesSettingsData> _entitiesSettings = new List<EntitiesSettingsData>();
        
        public EntitiesSettingsData GetEntityObject(EntityType entityType)
        {
            return _entitiesSettings.Find(v => v.EntityType == entityType);
        }
    }
    
    [Serializable]
    public class EntitiesSettingsData
    {
        public EntityType EntityType;
        public int HealtsPoints = 1;
        public float MoveSpeedEntity = 5f;
        public float RotationSpeedEntity = 10f;
        public int Damage = 1;
    }
}