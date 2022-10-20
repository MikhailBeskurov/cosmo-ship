using System;
using CosmoShip.Scripts.Modules.Player;
using CosmoShip.Scripts.Modules.Spawn;

namespace CosmoShip.Scripts.Modules.Gameplay
{
    public interface IGameplayModule
    {
        public event Action OnGameOver;
        public void StartGame();
    }

    public class GameplayModule : IGameplayModule, IDisposable
    {
        public event Action OnGameOver;
        
        private float _durationSpawnAsteroidsSec = 1.2f;
        private float _durationSpawnFlyingSaucerSec = 6f;
        
        private ISpawnModule _spawnModule;
        private IPlayerModule _playerModule;

        public GameplayModule(ISpawnModule spawnModule, IPlayerModule playerModule)
        {
            _playerModule = playerModule;
            _spawnModule = spawnModule;
        }

        public void StartGame()
        { 
            _playerModule.PlayerData.OnDestroy(() =>
            {
                OnGameOver?.Invoke();
            });
            _playerModule.SpawnPlayer();
            _spawnModule.StartSpawnAsteroids(_durationSpawnAsteroidsSec);
            _spawnModule.StartSpawnFlyingSaucer(_durationSpawnFlyingSaucerSec);
        }

        public void Dispose()
        {
            _spawnModule.StopSpawnAsteroids();
            _spawnModule.StopSpawnFlyingSaucer();
        }
    }
}