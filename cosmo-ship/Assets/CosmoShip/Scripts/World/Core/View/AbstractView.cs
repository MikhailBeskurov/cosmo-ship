using System;
using UnityEngine;

namespace CosmoShip.Scripts.World.Core.View
{
    public abstract class AbstractView : MonoBehaviour, IView
    {
        public abstract Type ModelType { get; }
        public abstract bool IsShown { get; }

        public abstract void Show();
        public abstract void Hide();
    }
}
