using CosmoShip.Scripts.ClientServices.RXExtension.Collections;
using CosmoShip.Scripts.Models.Entities;
using CosmoShip.Scripts.Modules.Damage;
using CosmoShip.Scripts.Modules.Entities;
using CosmoShip.Scripts.ScriptableObjects.Entities;
using CosmoShip.Scripts.Utils.Updatable;
using CosmoShip.Scripts.World.Core.Model;
using CosmoShip.Scripts.World.Views.Entities.Entity;
using UnityEngine;

namespace CosmoShip.Scripts.World.Models.Entities
{
    public class EntitiesModel : AbstractWorldViewModel
    {
        public readonly IReadOnlyReactiveCollections<EntityData> Entities;
        public readonly IUpdateModule UpdateModule;

        private EntitiesSettings _entitiesSettings;

        public EntitiesModel(IEntitiesModule entitiesModule, EntitiesSettings entitiesSettings, IUpdateModule updateModule)
        {
            _entitiesSettings = entitiesSettings;
            UpdateModule = updateModule;
            Entities = entitiesModule.Entities;
        }
        
        public BaseEntityView GetEntityObject(EntityType entityType)
        {
            return _entitiesSettings.GetEntityObject(entityType);
        }
    }
}