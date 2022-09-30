using System;
using CosmoShip.Scripts.UI.Core.Model;

namespace GethererHeroes.Scripts.UI.Core.View
{
    public abstract class BaseScreen<TModel> : AbstractScreen, IScreen<TModel> where TModel : IUIModel
    {
        public override Type ModelType => typeof(TModel);

        public abstract void Bind(TModel model);

        protected virtual void OnShowDone()
        {
        }

        protected virtual void OnHideDone()
        {
        }
    }
}
