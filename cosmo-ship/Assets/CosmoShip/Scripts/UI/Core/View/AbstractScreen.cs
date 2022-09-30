using System;
using UnityEngine;

namespace GethererHeroes.Scripts.UI.Core.View
{
    public abstract class AbstractScreen : MonoBehaviour, IScreen
    {
        public abstract Type ModelType { get; }
        public abstract bool IsShown { get; }
        public abstract void Show();

        public abstract void Hide();
    }
}
