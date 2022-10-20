using CosmoShip.Scripts.Models.Player;
using CosmoShip.Scripts.Models.Scenes;
using CosmoShip.Scripts.Modules.Scenes;
using CosmoShip.Scripts.UI.Core.Model;

namespace CosmoShip.Scripts.UI.Models.MainMenu
{
    public class MainMenuModel : AbstractScreenModel
    {
        public int MaxScorePlayer => _playerScoreData.GetMaxScore;
        
        private IScenesModule _scenesModule;
        private PlayerScoreData _playerScoreData;

        public MainMenuModel(IScenesModule scenesModule, PlayerScoreData playerScoreData)
        {
            _playerScoreData = playerScoreData;
            _scenesModule = scenesModule;
        }

        public void LoadBattleScene()
        {
            _scenesModule.LoadNewScene(ScenesData.BattleScene);
        }
    }
}