using CosmoShip.Scripts.ClientServices;
using CosmoShip.Scripts.ClientServices.DIContainer;
using CosmoShip.Scripts.Models.BorderMap;
using CosmoShip.Scripts.Modules.Damage;
using CosmoShip.Scripts.Modules.Entities;
using CosmoShip.Scripts.Modules.Player;
using CosmoShip.Scripts.Modules.Spawn;
using CosmoShip.Scripts.ScriptableObjects.Bullets;
using CosmoShip.Scripts.ScriptableObjects.Entities;
using CosmoShip.Scripts.ScriptableObjects.Player;
using CosmoShip.Scripts.UI.Core;
using CosmoShip.Scripts.Utils.Updatable;
using CosmoShip.Scripts.World.Core;
using CosmoShip.Scripts.World.Models.Entities;
using CosmoShip.Scripts.World.Models.Player;
using UnityEngine;

namespace CosmoShip.Scripts.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private UpdatableModule _updateModule;
        [SerializeField] private EntitiesObjects _entitiesObjects;
        [SerializeField] private EntitiesSettings _entitiesSettings;
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private BulletsSettings _bulletsSettings;
        
        private GameResourcesManager _gameResourcesManager;
        private AssetProvider _assetProvider;
        private UIManager _uiManager;
        private WorldManager _worldManager;
        
        private BorderMapData _borderMapData;
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
            InitScriptableObjects();
            InitModules();
            InitUI();
            InitWorld();
        }
        
        private void InitManagers()
        {
            _uiManager = new UIManager(_assetProvider, _canvas);
            _worldManager = new WorldManager(_assetProvider);
            
            DiContainer.BindFromInstance<UIManager>(_uiManager);
            DiContainer.BindFromInstance<WorldManager>(_worldManager);
        }

        private void InitModules()
        {
            _borderMapData = new BorderMapData(_canvas.pixelRect);
            
            DiContainer.BindFromInstance<AssetProvider>(_assetProvider);
            DiContainer.BindFromInstance<BorderMapData>(_borderMapData);
            DiContainer.BindFromInstance<UpdatableModule>(_updateModule);
            
            DiContainer.BindFromNew<PlayerInputControls>();
            DiContainer.BindFromNew<PlayerModule>();
            DiContainer.BindFromNew<PlayerShootingModule>();
            DiContainer.BindFromNew<DamageModule>();
            DiContainer.BindFromNew<EntitiesModule>();
            DiContainer.BindFromNew<SpawnModule>();
        }
        
        private void InitScriptableObjects()
        { 
            DiContainer.BindFromInstance<EntitiesObjects>(_entitiesObjects);
            DiContainer.BindFromInstance<EntitiesSettings>(_entitiesSettings);
            DiContainer.BindFromInstance<PlayerSettings>(_playerSettings);
            DiContainer.BindFromInstance<BulletsSettings>(_bulletsSettings);
        }
        
        private void InitUI()
        {
        }
        
        private void InitWorld()
        { 
            DiContainer.BindFromNew<EntitiesModel>();
            DiContainer.BindFromNew<PlayerModel>();
            
            _worldManager.Bind<EntitiesModel>(DiContainer.GetInstance<EntitiesModel>()); 
            _worldManager.BindAndHide<PlayerModel>(DiContainer.GetInstance<PlayerModel>()); 
        }
    }
}
