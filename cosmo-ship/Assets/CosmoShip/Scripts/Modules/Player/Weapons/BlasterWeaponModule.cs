using System;
using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using CosmoShip.Scripts.Models.Bullets;
using CosmoShip.Scripts.ScriptableObjects.Bullets;

namespace CosmoShip.Scripts.Modules.Player.Weapons
{   
    public interface IBlasterWeaponModule
    {
        public Action<BulletData> OnShot { get; set; }
    }
    public class BlasterWeaponModule : IBlasterWeaponModule, IDisposable
    {        
        public Action<BulletData> OnShot { get; set; }

        private PlayerInputControls _playerInputControls;
        private BulletsSettings _bulletsSettings;
        
        public BlasterWeaponModule(PlayerInputControls playerInputControls, BulletsSettings bulletsSettings)
        {
            _bulletsSettings = bulletsSettings;
            _playerInputControls = playerInputControls;
            Init();
        }

        private void Init()
        {
            BindBlasterShoot();
        }
        
        private void BindBlasterShoot()
        {
            _playerInputControls.SpaceShip.BlasterShoot.performed += context =>
            {
                ShotWeapon(BulletTypes.Blaster);
            };
        }
        
        private void ShotWeapon(BulletTypes bulletType)
        {
            OnShot?.Invoke(_bulletsSettings.GetBulletData(bulletType));
        }

        public void Dispose()
        {
            _playerInputControls?.Dispose();
        }
    }
}