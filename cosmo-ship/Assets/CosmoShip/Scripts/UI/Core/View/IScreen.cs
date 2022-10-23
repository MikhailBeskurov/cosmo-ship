using CosmoShip.Scripts.UI.Core.Model;

namespace CosmoShip.Scripts.UI.Core.View
{
    public interface IScreen
    {
        bool IsShown { get; }
        void Show();
        void Hide();
    }

    public interface IScreen<TModel> : IScreen where TModel : IUIModel
    {
        void Bind(TModel model);
    }
}
