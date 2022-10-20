using CosmoShip.Scripts.ClientServices;
using CosmoShip.Scripts.ClientServices.DIContainer;
using CosmoShip.Scripts.Models.Player;
using CosmoShip.Scripts.Modules.Scenes;
using CosmoShip.Scripts.UI.Core;
using CosmoShip.Scripts.UI.Models.MainMenu;
using CosmoShip.Scripts.World.Core;
using UnityEngine;

namespace CosmoShip.Scripts.Installers
{
    
    public class MainMenuInstaller : MonoInstaller
    {   
        [SerializeField] private Canvas _canvas;
        
        private GameResourcesManager _gameResourcesManager;
        private AssetProvider _assetProvider;
        private UIManager _uiManager;
        
        private readonly string[] _atlasGroups = {};
        
        protected override void InstallBindings()
        {
            base.InstallBindings();
            LoadResources();
        }
        
        private void LoadResources()
        {
            _gameResourcesManager = new GameResourcesManager();
            _assetProvider = new AssetProvider(_gameResourcesManager);
            
            SpriteProvider.Init(_gameResourcesManager);
            _gameResourcesManager.LoadGroups(_atlasGroups, Init);
        }
        
        private void Init()
        {
            InitManagers();
            InitModules();
            InitUI();
        }
        
        private void InitManagers()
        {
            _uiManager = new UIManager(_assetProvider, _canvas);
            
            DiContainer.BindFromInstance<UIManager>(_uiManager);
        }

        private void InitModules()
        {
            DiContainer.BindFromNew<ScenesModule>();
            DiContainer.BindFromNew<PlayerScoreData>();
        }

        private void InitUI()
        {
            DiContainer.BindFromNew<MainMenuModel>();
            
            _uiManager.Bind(DiContainer.GetInstance<MainMenuModel>());
        }
    }
}