using System;
using CosmoShip.Scripts.ClientServices.DIContainer;
using CosmoShip.Scripts.UI.Views;
using CosmoShip.Scripts.Utils.RXExtension;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace CosmoShip.Scripts.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        protected override void InstallBindings()
        {
            base.InstallBindings();
            Init();
        }

        private void Init()
        {
           
        }
    }
}
