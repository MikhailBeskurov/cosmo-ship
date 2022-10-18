using System;
using CosmoShip.Scripts.ClientServices.RXExtension.Collections;
using CosmoShip.Scripts.Models.Entities;
using CosmoShip.Scripts.Modules.Damage;
using CosmoShip.Scripts.Modules.Spawn;
using UnityEngine;

namespace CosmoShip.Scripts.Modules.Entities
{
    public interface IEntitiesModule
    {
        public IReadOnlyReactiveCollections<EntityData> Entities { get; }
    }

    public class EntitiesModule: IEntitiesModule, IDisposable
    {
        public IReadOnlyReactiveCollections<EntityData> Entities => _entities;

        private ReactiveCollections<EntityData> _entities = new ReactiveCollections<EntityData>();
        private ISpawnModule _spawnModule;

        public EntitiesModule(ISpawnModule spawnModule)
        {
            _spawnModule = spawnModule;
            _spawnModule.OnAddEntity += AddEntity;
        }

        private void AddEntity(EntityData entityData)
        {
            entityData.OnDestroy(() => RemoveEntity(entityData));
            _entities.Add(entityData);
        }

        private void RemoveEntity(EntityData entityData)
        {
            _entities.Remove(entityData);
        }
        
        public void Dispose()
        {
            _spawnModule.OnAddEntity -= AddEntity;
        }
    }
}