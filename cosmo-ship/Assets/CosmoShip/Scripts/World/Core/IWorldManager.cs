using CosmoShip.Scripts.World.Core.Model;
using CosmoShip.Scripts.World.Core.View;

namespace CosmoShip.Scripts.World.Core
{
    public interface IWorldManager
    {
        void Show<T>() where T : IViewModel;
        void Show<T>(string viewName) where T : IViewModel;
        void Bind<T>(T model) where T : IViewModel;
        void ShowAndBind<T>(T model) where T : IViewModel;
        void ShowAndBind<T>(T model, string viewName) where T : IViewModel;
        void Hide<T>() where T : IViewModel;
        bool IsShown<T>() where T : IViewModel;
        bool IsShown(string viewName);
        IView GetViewInstance<IView>() where IView : AbstractView;
    }
}
