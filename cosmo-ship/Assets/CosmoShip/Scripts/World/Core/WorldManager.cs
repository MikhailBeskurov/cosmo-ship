using System.Collections.Generic;
using System.Linq;
using CosmoShip.Scripts.ClientServices;
using CosmoShip.Scripts.World.Core.Model;
using CosmoShip.Scripts.World.Core.View;
using UnityEngine;

namespace CosmoShip.Scripts.World.Core
{
    public class WorldManager : IWorldManager
    {
        private readonly IAssetProvider _assetProvider;
        private readonly List<AbstractView> _views = new List<AbstractView>();

        public WorldManager(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void Show<T>() where T : IViewModel
        {
            var view = GetView<T>();
            view.Show();
        }

        public void Show<T>(string viewName) where T : IViewModel
        {
            var view = GetView<T>(viewName);
            view.Show();
        }

        public void Bind<T>(T model) where T : IViewModel
        {
            var view = GetView<T>();
            model.SetManager(this);
            view.Bind(model);
        }

        public void ShowAndBind<T>(T model) where T : IViewModel
        {
            var view = GetView<T>();
            model.SetManager(this);
            view.Bind(model);
            view.Show();
        }

        public void ShowAndBind<T>(T model, string viewName) where T : IViewModel
        {
            var view = GetView<T>(viewName);
            model.SetManager(this);
            view.Bind(model);
            view.Show();
        }
        
        public void BindAndHide<T>(T model) where T : IViewModel
        {
            var view = GetView<T>();
            model.SetManager(this);
            view.Bind(model);
            view.Hide();
        }

        public void BindAndHide<T>(T model, string viewName) where T : IViewModel
        {
            var view = GetView<T>(viewName);
            model.SetManager(this);
            view.Bind(model);
            view.Hide();
        }
        
        public IView GetViewInstance<IView>() where IView : AbstractView
        {
            return (IView)_views.FirstOrDefault(e => e.GetType() == typeof(IView));
        }

        public void Hide<T>() where T : IViewModel
        {
            var targetType = typeof(T);
            var targetViews = _views.Where(v => v.ModelType.IsAssignableFrom(targetType));

            foreach (var view in targetViews)
            {
                view.Hide();
            }
        }

        public bool IsShown<T>() where T : IViewModel
        {
            var targetType = typeof(T);
            var view = _views.FirstOrDefault(v => v.ModelType.IsAssignableFrom(targetType));

            return view != null && view.IsShown;
        }

        public bool IsShown(string viewName)
        {
            return _views.FirstOrDefault(v => v.name == viewName)?.IsShown ?? false;
        }

        private IView<T> GetView<T>() where T : IViewModel
        {
            var targetType = typeof(T);
            var createdView = _views.FirstOrDefault(v => v.ModelType.IsAssignableFrom(targetType));

            if (createdView != null)
            {
                return createdView as IView<T>;
            }

            var view = _assetProvider.GetPrefab<T>();
            view = Object.Instantiate(view);
            _views.Add(view);

            return view as IView<T>;
        }

        private IView<T> GetView<T>(string prefabName) where T : IViewModel
        {
            var targetType = typeof(T);
            var createdView =
                _views.FirstOrDefault(v => v.ModelType.IsAssignableFrom(targetType) && v.name == prefabName);

            if (createdView != null)
            {
                return createdView as IView<T>;
            }

            var view = _assetProvider.GetPrefab<T>(prefabName);
            view = Object.Instantiate(view);
            _views.Add(view);

            return view as IView<T>;
        }
    }
}
