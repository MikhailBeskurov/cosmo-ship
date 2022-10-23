using System.Collections.Generic;
using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using CosmoShip.Scripts.Models.Movement;
using CosmoShip.Scripts.ScriptableObjects.Bullets;
using CosmoShip.Scripts.World.Views.Bullets;
using UnityEngine;

namespace CosmoShip.Scripts.Models.Bullets
{
    public class BulletData : MovementData
    {
        public BaseBulletView BulletView => _bulletView;
        public BulletTypes BulletType => _bulletType;
        public int Damage => _damage;
        
        private BaseBulletView _bulletView;
        private BulletTypes _bulletType;
        private int _damage;
        
        public BulletData(BulletSettingsData bulletsSettings)
        {
            _bulletView = bulletsSettings.BulletView;
            _bulletType = bulletsSettings.BulletType;
            _damage = bulletsSettings.Damage;
            InitSettingsMovement(bulletsSettings.MovementSettings);
        }
    }
}