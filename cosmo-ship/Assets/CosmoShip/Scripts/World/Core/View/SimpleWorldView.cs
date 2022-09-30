using CosmoShip.Scripts.World.Core.Model;

namespace CosmoShip.Scripts.World.Core.View
{
    public abstract class SimpleWorldView<T> : BaseView<T> where T : IViewModel
    {
        public override bool IsShown => gameObject.activeSelf;

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
