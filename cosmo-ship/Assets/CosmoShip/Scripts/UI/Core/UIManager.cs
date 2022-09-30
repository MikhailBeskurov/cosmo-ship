using System.Collections.Generic;
using System.Linq;
using CosmoShip.Scripts.ClientServices;
using CosmoShip.Scripts.UI.Core.Model;
using GethererHeroes.Scripts.UI.Core;
using GethererHeroes.Scripts.UI.Core.View;
using UnityEngine;

namespace CosmoShip.Scripts.UI.Core
{
    public class UIManager : IUIManager
    {
        private Canvas _canvas;

        private List<AbstractScreen> _screens = new List<AbstractScreen>();

        private IAssetProvider _assetProvider;
        
        public UIManager(IAssetProvider assetProvider, Canvas canvas)
        {
            _assetProvider = assetProvider;
            _canvas = canvas;
        }
       
        public void DisableAllScreen()
        {
            foreach (var screen in _screens)
            {
                screen.Hide();
            }
        }

        public void Show<T>() where T : IUIModel
        {
            var screen = GetScreen<T>();
            screen.Show();
        }
        public void Show<T>(string windowName) where T : IUIModel
        {
            var screen = GetScreen<T>(windowName);
            screen.Show();
        }

        public void Bind<T>(T model) where T : IUIModel
        {
            var screen = GetScreen<T>();
            model.SetManager(this);
            screen.Bind(model);
        }

        public void BindAndHide<T>(T model) where T : IUIModel
        {
            var screen = GetScreen<T>();
            model.SetManager(this);
            screen.Bind(model);
            screen.Hide();
        }

        public void ShowAndBind<T>(T model) where T : IUIModel
        {
            var screen = GetScreen<T>();
            model.SetManager(this);
            screen.Bind(model);
            screen.Show();
        }

        public void ShowAndBind<T>(T model, string windowName) where T : IUIModel
        {
            var screen = GetScreen<T>(windowName);
            model.SetManager(this);
            screen.Bind(model);
            screen.Show();
        }
        
        public TScreen GetScreenInstance<TScreen>() where TScreen : AbstractScreen
        {
            return (TScreen)_screens.FirstOrDefault(e => e.GetType() == typeof(TScreen));
        }

        public void Hide<T>() where T : IUIModel
        {
            var targetType = typeof(T);
            var screens = _screens.Where(v => v.ModelType.IsAssignableFrom(targetType));
            foreach (var screen in screens)
            {
                screen.Hide();
            }
        }

        public bool IsShown<T>() where T : IUIModel
        {
            var targetType = typeof(T);
            var createdScreen = _screens.FirstOrDefault(v => v.ModelType.IsAssignableFrom(targetType));
            return createdScreen != null && createdScreen.IsShown;
        }

        public bool IsShown(string windowName)
        {
            return _screens.FirstOrDefault(e => e.name == windowName)?.IsShown ?? false;
        }

        private IScreen<T> GetScreen<T>() where T : IUIModel
        {
            var targetType = typeof(T);
            
            var createdScreen = _screens.FirstOrDefault(v => v.ModelType.IsAssignableFrom(targetType));
            if (createdScreen != null)
            {
                return createdScreen as IScreen<T>;
            }
            var screen = _assetProvider.GetWindow<T>();
            screen = Object.Instantiate(screen, _canvas.transform);
            _screens.Add(screen);
            return screen as IScreen<T>;
        }

        private IScreen<T> GetScreen<T>(string windowName) where T : IUIModel
        {
            var targetType = typeof(T);
            var createdScreen = _screens.FirstOrDefault(v => v.ModelType.IsAssignableFrom(targetType) && v.name == windowName);
            if (createdScreen != null)
            {
                return createdScreen as IScreen<T>;
            }
            
            var screen = _assetProvider.GetWindow<T>(windowName);
            screen = Object.Instantiate(screen, _canvas.transform);
            _screens.Add(screen);
            return screen as IScreen<T>;
        }
    }
}
