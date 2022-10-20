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
        public void StartSpawnAsteroids(float durationSpawn);
        public void StopSpawnAsteroids();
        public void StartSpawnFlyingSaucer(float durationSpawn);
        public void StopSpawnFlyingSaucer();
    }
    
    public class SpawnModule : ISpawnModule
    {
        public event Action<EntityData> OnAddEntity;
        
        private BorderMapData _borderMapData;
        private EntitiesSettings _entitiesSettings;
        private IPlayerMovementModule _playerMovementModule;

        private Task _spawnAsteroids;
        private Task _spawnFlyingSaucer;
        
        public SpawnModule(BorderMapData borderMapData, EntitiesSettings entitiesSettings, 
            IPlayerMovementModule playerMovementModule)
        {
            _playerMovementModule = playerMovementModule;
            _entitiesSettings = entitiesSettings;
            _borderMapData = borderMapData;
        }

        public void StartSpawnAsteroids(float durationSpawn)
        {
            StopSpawnAsteroids();
            float durationMilSec = durationSpawn * 1000;
            _spawnAsteroids = СyclicSpawnAsteroids((int)durationMilSec);
        }

        public void StopSpawnAsteroids()
        {
            if (_spawnAsteroids != null && _spawnAsteroids.Status == TaskStatus.RanToCompletion)
            {
                _spawnAsteroids.Dispose();
            }
        }

        public void StartSpawnFlyingSaucer(float durationSpawn)
        {
            StopSpawnFlyingSaucer();
            float durationMilSec = durationSpawn * 1000;
            _spawnFlyingSaucer = СyclicSpawnFlyingSaucer((int)durationMilSec);
        }

        public void StopSpawnFlyingSaucer()
        {
            if (_spawnFlyingSaucer != null && _spawnFlyingSaucer.Status == TaskStatus.RanToCompletion)
            {
                _spawnFlyingSaucer.Dispose();
            }
        }

        private async Task СyclicSpawnAsteroids(int durationSpawn)
        {
            while (true)
            {
                var rnd = Random.Range(0, 2);
                var initPosition = GetInitPosition();
                var initDirectionMove = Quaternion.AngleAxis(Random.Range(-30f, 30f), Vector3.forward) *
                                        (_playerMovementModule.PositionPlayer.Value - initPosition).normalized;
                switch (rnd)
                {
                    case 0:
                        SpawnBigAsteroids(initPosition,initDirectionMove);
                        break;
                    case 1:
                        SpawnMediumAsteroids(initPosition,initDirectionMove);
                        break;
                }
                await Task.Delay(durationSpawn);
            }
        }
        
        private async Task СyclicSpawnFlyingSaucer(int durationSpawn)
        {
            while (true)
            {
                SpawnFlyingSaucer(GetInitPosition());
                await Task.Delay(durationSpawn);
            }
        }
        
        private void SpawnBigAsteroids(Vector2 baseInitPosition, Vector2 baseDirectionMove)
        {
            var entity = new EntityData(_entitiesSettings.GetEntitySettings(EntityType.BigAsteroid), baseInitPosition,
                baseDirectionMove);
            entity.OnDestroy(() => SpawnMediumAsteroids(entity.CurrentPosition, baseDirectionMove));
            OnAddEntity?.Invoke(entity);
        }
        
        private void SpawnMediumAsteroids(Vector2 baseInitPosition, Vector2 baseDirectionMove)
        {
            EntityData entity = new EntityData(_entitiesSettings.GetEntitySettings(EntityType.MediumAsteroid), baseInitPosition,
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
                var entity = new EntityData(_entitiesSettings.GetEntitySettings(EntityType.SmallAsteroid), baseInitPosition,
                    directionMove);
                OnAddEntity?.Invoke(entity);
            }
        }
        
        private EntityData SpawnFlyingSaucer(Vector2 baseInitPosition)
        {
            EntityData entity = new EntityData(_entitiesSettings.GetEntitySettings(EntityType.FlyingSauce), baseInitPosition,
                _playerMovementModule.PositionPlayer.Value);
            entity.SubscribeDirectionMove(_playerMovementModule.PositionPlayer);
            OnAddEntity?.Invoke(entity);
            return entity;
        }
        
        private Vector2 GetInitPosition()
        {
            float widht = _borderMapData.Widht;
            float height = _borderMapData.Height;
            int rnd = Random.Range(-1,1);
            Vector2 initPosition = Vector2.zero;
            
            if (rnd == -1)
            {
                float rndHeight = ((Random.Range(-1, 1) < 0) ? -1 : 1) * height;
                initPosition = new Vector2(Random.Range(-widht,widht),rndHeight);
            }
            else
            {  
                float rndWidht = ((Random.Range(-1, 1) < 0) ? -1 : 1) * widht;
                initPosition = new Vector2(rndWidht,Random.Range(-height,height));
            }
            return initPosition;
        }
    }
}