using System;
using CosmoShip.Scripts.ClientServices.RXExtension.Collections;
using CosmoShip.Scripts.Models.BorderMap;
using CosmoShip.Scripts.Models.Entities;
using CosmoShip.Scripts.Modules.Damage;
using CosmoShip.Scripts.Modules.Spawn;
using CosmoShip.Scripts.Utils.Updatable;
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
        private BorderMapData _borderMapData;

        public EntitiesModule(ISpawnModule spawnModule, IUpdateModule updatableModule, BorderMapData borderMapData)
        {
            _borderMapData = borderMapData;
            updatableModule.AddAction(Updatable);
            _spawnModule = spawnModule;
            _spawnModule.OnAddEntity += AddEntity;
        }

        private void Updatable(float deltaTime)
        {
            for (int i = 0; i < _entities.Values.Count; i++)
            {
                if (CheckOutOfPermissibleArea(Entities.Values[i].CurrentPosition))
                {
                    RemoveEntity(Entities.Values[i]);
                }
            }
        }

        private bool CheckOutOfPermissibleArea(Vector2 currentPosition)
        {
            float widht = _borderMapData.Widht * 2;
            float height = _borderMapData.Height * 2;
            
            if(currentPosition.x > widht || currentPosition.x < -widht ||
               currentPosition.y > height || currentPosition.y < -height)
            {
                return true;
            }

            return false;
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