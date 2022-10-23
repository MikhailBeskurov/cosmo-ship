using CosmoShip.Scripts.UI.Core.Model;
using CosmoShip.Scripts.UI.Core.View;

namespace CosmoShip.Scripts.UI.Core
{
    public interface IUIManager
    {
        void Show<T>() where T : IUIModel;
        void Show<T>(string windowName) where T : IUIModel;
        void Bind<T>(T model) where T : IUIModel;
        void ShowAndBind<T>(T model) where T : IUIModel;
        void ShowAndBind<T>(T model, string windowName) where T : IUIModel;
        TScreen GetScreenInstance<TScreen>() where TScreen : AbstractScreen;
        void Hide<T>() where T : IUIModel;
        bool IsShown<T>() where T : IUIModel;
        bool IsShown(string windowName);
    }
}