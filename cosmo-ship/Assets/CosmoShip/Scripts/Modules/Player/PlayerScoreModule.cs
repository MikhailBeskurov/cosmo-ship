using System;
using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using CosmoShip.Scripts.Models.Entities;
using CosmoShip.Scripts.Models.Player;
using CosmoShip.Scripts.Modules.Gameplay;
using CosmoShip.Scripts.Modules.Spawn;
using UnityEngine;

namespace CosmoShip.Scripts.Modules.Player
{
    public interface IPlayerScoreModule
    {
        public IReadOnlyReactiveProperty<int> ScorePlayer { get; }
    }

    public class PlayerScoreModule : IPlayerScoreModule, IDisposable
    {
        public IReadOnlyReactiveProperty<int> ScorePlayer => _scorePlayer;

        private ReactiveProperty<int> _scorePlayer = new ReactiveProperty<int>();
        private ISpawnModule _spawnModule;
        private IGameplayModule _gameplayModule;
        private PlayerScoreData _playerScoreData;

        public PlayerScoreModule(ISpawnModule spawnModule, IGameplayModule gameplayModule, PlayerScoreData playerScoreData)
        {
            _playerScoreData = playerScoreData;
            _gameplayModule = gameplayModule;
            _spawnModule = spawnModule;

            spawnModule.OnAddEntity += AddEntity;
            gameplayModule.OnGameOver += SaveMaxScore;
        }

        private void AddEntity(EntityData entityData)
        {
            entityData.OnDestroy(() => { _scorePlayer.Value += entityData.ScoreOnDestroy; });
        }

        private void SaveMaxScore()
        {
            if (_playerScoreData.GetMaxScore < ScorePlayer.Value)
            {
                _playerScoreData.GetMaxScore = ScorePlayer.Value;
            }
        }

        public void Dispose()
        {
            _spawnModule.OnAddEntity -= AddEntity;
            _gameplayModule.OnGameOver -= SaveMaxScore;
        }
    }
}