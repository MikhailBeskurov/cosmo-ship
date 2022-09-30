using CosmoShip.Scripts.UI.Core.Model;
using CosmoShip.Scripts.World.Core.Model;
using CosmoShip.Scripts.World.Core.View;
using GethererHeroes.Scripts.UI.Core.View;
using UnityEngine;

namespace CosmoShip.Scripts.ClientServices
{
    public interface IAssetProvider
    {
        AbstractScreen GetWindow<T>(string name) where T : IUIModel;
        AbstractScreen GetWindow<T>() where T : IUIModel;
        T GetComponent<T>() where T : Component;
        T GetComponent<T>(string name) where T : Component;
        AbstractView GetPrefab<T>() where T : IViewModel;
        AbstractView GetPrefab<T>(string prefabName) where T : IViewModel;
    }
    
    public class AssetProvider : IAssetProvider
    {
        private IGameResourcesManager _gameResourcesManager;

        public AssetProvider(IGameResourcesManager gameResourcesManager)
        {
            _gameResourcesManager = gameResourcesManager;
        }

        public AbstractScreen GetWindow<T>(string name) where T : IUIModel
        {
            return _gameResourcesManager.GetPrefab<AbstractScreen>(name);
        }

        public AbstractScreen GetWindow<T>() where T : IUIModel
        {
            return _gameResourcesManager.GetPrefab<AbstractScreen>(typeof(T).Name);
        }

        public T GetComponent<T>() where T : Component
        {
            return _gameResourcesManager.GetPrefab<T>(typeof(T).Name);
        }

        public T GetComponent<T>(string name) where T : Component
        {
            return _gameResourcesManager.GetPrefab<T>(name);
        }

        public AbstractView GetPrefab<T>() where T : IViewModel
        {
            return _gameResourcesManager.GetPrefab<AbstractView>(typeof(T).Name);
        }

        public AbstractView GetPrefab<T>(string prefabName) where T : IViewModel
        {
            return _gameResourcesManager.GetPrefab<AbstractView>(prefabName);
        }
    }
}