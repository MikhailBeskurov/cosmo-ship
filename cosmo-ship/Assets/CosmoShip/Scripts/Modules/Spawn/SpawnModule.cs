using System;
using System.Threading.Tasks;
using CosmoShip.Scripts.Models.BorderMap;
using CosmoShip.Scripts.Models.Entities;
using CosmoShip.Scripts.Modules.Player;
using CosmoShip.Scripts.ScriptableObjects.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CosmoShip.Scripts.Modules.Spawn
{
    public interface ISpawnModule
    {
        public event Action<EntityData> OnAddEntity;
    }
    
    public class SpawnModule : ISpawnModule
    {
        public event Action<EntityData> OnAddEntity;
        
        private Vector2 _positionPlayer;
        private BorderMapData _borderMapData;
        private EntitiesSettings _entitiesSettings;
        private IPlayerModule _playerModule;

        public SpawnModule(BorderMapData borderMapData, EntitiesSettings entitiesSettings, IPlayerModule playerModule)
        {
            _playerModule = playerModule;
            _entitiesSettings = entitiesSettings;
            _borderMapData = borderMapData;
            var startSpawn = StartSpawn();
        }

        private async Task StartSpawn()
        {
            await Task.Delay(1500);
            while (true)
            {
                var rnd = Random.Range(0, 3);
                var initPosition = GetInitPosition();
                var initDirectionMove = Quaternion.AngleAxis(Random.Range(-20f, 20f), Vector3.forward) *
                                        (_positionPlayer - initPosition).normalized;
                switch (rnd)
                {
                    case 0:
                        SpawnBigAsteroids(initPosition,initDirectionMove);
                        break;
                    case 1:
                        SpawnMediumAsteroids(initPosition,initDirectionMove);
                        break;
                    case 2:
                        SpawnFlyingSaucer(initPosition);
                        break;
                }
                await Task.Delay(1500);
            }
        }

        private void SpawnBigAsteroids(Vector2 baseInitPosition, Vector2 baseDirectionMove)
        {
            var entity = new EntityData(_entitiesSettings.GetEntityObject(EntityType.BigAsteroid), baseInitPosition,
                baseDirectionMove);
            entity.OnDestroy(() => SpawnMediumAsteroids(entity.CurrentPosition, baseDirectionMove));
            OnAddEntity?.Invoke(entity);
        }
        
        private void SpawnMediumAsteroids(Vector2 baseInitPosition, Vector2 baseDirectionMove)
        {
            EntityData entity = new EntityData(_entitiesSettings.GetEntityObject(EntityType.MediumAsteroid), baseInitPosition,
                  baseDirectionMove);
            entity.OnDestroy(() => SpawnSmallAsteroids(entity.CurrentPosition, baseDirectionMove));
            OnAddEntity?.Invoke(entity);
        }
        
        private void SpawnSmallAsteroids(Vector2 baseInitPosition, Vector2 baseDirectionMove)
        {
            int count = 2;
            float angel = 90 / count;
            
            for (int i = 1; i <= count; i++)
            { 
                int charValue = i % 2 == 0 ? -1 : 0;
                var directionMove = Quaternion.AngleAxis(charValue * angel, Vector3.forward) * baseDirectionMove;
                var entity = new EntityData(_entitiesSettings.GetEntityObject(EntityType.SmallAsteroid), baseInitPosition,
                    directionMove);
                OnAddEntity?.Invoke(entity);
            }
        }
        
        private void SpawnFlyingSaucer(Vector2 baseInitPosition)
        {
            EntityData entity = new EntityData(_entitiesSettings.GetEntityObject(EntityType.FlyingSauce), baseInitPosition,
                _playerModule.PositionPlayer.Value);
            entity.SubscribeDirectionMove(_playerModule.PositionPlayer);
            OnAddEntity?.Invoke(entity);
        }
        
        private Vector2 GetInitPosition()
        {
            float randomPosX = Random.Range(-1f,1f);
            float randomPosY = Random.Range(-1f,1f);
            return new Vector2(_positionPlayer.x + randomPosX * (_borderMapData.Widht/ 20),
                _positionPlayer.y + randomPosY * (_borderMapData.Height/ 20));
        }
    }
}