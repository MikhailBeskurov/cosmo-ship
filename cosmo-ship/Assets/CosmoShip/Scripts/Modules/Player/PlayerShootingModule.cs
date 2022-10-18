using System;
using CosmoShip.Scripts.Models.Bullets;
using CosmoShip.Scripts.ScriptableObjects.Bullets;

namespace CosmoShip.Scripts.Modules.Player
{
    public interface IPlayerShootingModule
    {
        public Action<BulletData> OnShot { get; set; }
    }
    
    public class PlayerShootingModule : IPlayerShootingModule
    {
        public Action<BulletData> OnShot { get; set; }
        private PlayerInputControls _playerInputControls;
        private readonly BulletsSettings _bulletsSettings;

        public PlayerShootingModule(PlayerInputControls playerInputControls, BulletsSettings bulletsSettings)
        {
            _playerInputControls = playerInputControls;
            _bulletsSettings = bulletsSettings;
            BindBlasterShoot();
            BindLaserShoot();
        }

        private void BindBlasterShoot()
        {
            _playerInputControls.SpaceShip.BlasterShoot.performed += context =>
            {
                OnShot?.Invoke(BlasterShot(BulletTypes.Blaster));
            };
        }
        
        private void BindLaserShoot()
        {
            _playerInputControls.SpaceShip.LaserShoot.performed += context =>
            {
                OnShot?.Invoke(BlasterShot(BulletTypes.Laser));
            };
        }
        
        private BulletData BlasterShot(BulletTypes bulletType)
        {
            return _bulletsSettings.GetBulletData(bulletType);
        }
    }
}