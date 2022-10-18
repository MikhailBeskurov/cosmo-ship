using System;
using System.Collections.Generic;
using CosmoShip.Scripts.Models.Entities;
using CosmoShip.Scripts.World.Views.Entities.Entity;
using UnityEngine;

namespace CosmoShip.Scripts.ScriptableObjects.Entities
{
    [CreateAssetMenu(fileName = "EntitiesObjects", menuName = "Objects/EntitiesObjects", order = 0)]
    public class EntitiesObjects : ScriptableObject
    {
        [SerializeField] private List<EntitiesObjectData> _entitiesObject = new List<EntitiesObjectData>();

        public BaseEntityView GetEntityObject(EntityType entityType)
        {
            return _entitiesObject.Find(v => v.EntityType == entityType).EntityView;
        }
    }

    [Serializable]
    public class EntitiesObjectData
    {
        public BaseEntityView EntityView;
        public EntityType EntityType;
    }
}