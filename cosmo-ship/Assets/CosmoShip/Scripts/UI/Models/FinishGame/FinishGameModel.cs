using System;
using CosmoShip.Scripts.Models.Player;
using CosmoShip.Scripts.Models.Scenes;
using CosmoShip.Scripts.Modules.Gameplay;
using CosmoShip.Scripts.Modules.Player;
using CosmoShip.Scripts.Modules.Scenes;
using CosmoShip.Scripts.UI.Core.Model;

namespace CosmoShip.Scripts.UI.Models.FinishGame
{
    public class FinishGameModel : AbstractScreenModel, IDisposable
    {
        public Action OnGameOver;
        public int LastResultScore = 0;
        public int CurrentScorePlayer => _playerScoreModule.ScorePlayer.Value;
        
        private IScenesModule _scenesModule;
        private IPlayerScoreModule _playerScoreModule;
        private IGameplayModule _gameplayModule;

        public FinishGameModel(IScenesModule scenesModule, IPlayerScoreModule playerScoreModule, 
            PlayerScoreData playerScoreData, IGameplayModule gameplayModule)
        {
            _gameplayModule = gameplayModule;
            _playerScoreModule = playerScoreModule;
            LastResultScore = playerScoreData.GetMaxScore;
            _scenesModule = scenesModule;
            gameplayModule.OnGameOver += GameOver;
        }

        public void LoadMainMenuScene()
        {
            _scenesModule.LoadNewScene(ScenesData.MainMenuScene);
        }

        private void GameOver()
        {
            OnGameOver?.Invoke();
        }
        
        public void Dispose()
        {
            _gameplayModule.OnGameOver -= GameOver;
        }
    }
}