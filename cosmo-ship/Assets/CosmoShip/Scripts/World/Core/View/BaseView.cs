using System;
using CosmoShip.Scripts.World.Core.Model;

namespace CosmoShip.Scripts.World.Core.View
{
    public abstract class BaseView<T> : AbstractView, IView<T> where T : IViewModel
    {
        public override Type ModelType => typeof(T);

        public abstract void Bind(T model);

        protected virtual void OnShowDone()
        {
        }

        protected virtual void OnHideDone()
        {
        }
    }
}
