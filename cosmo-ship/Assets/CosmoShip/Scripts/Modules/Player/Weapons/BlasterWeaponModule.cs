using System;
using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using CosmoShip.Scripts.Models.Bullets;
using CosmoShip.Scripts.Models.Movement;
using CosmoShip.Scripts.ScriptableObjects.Bullets;
using UnityEngine;

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
        private IPlayerMovementModule _playerMovementModule;

        public BlasterWeaponModule(PlayerInputControls playerInputControls, BulletsSettings bulletsSettings, 
            IPlayerMovementModule playerMovementModule)
        {
            _playerMovementModule = playerMovementModule;
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
            var bullet = new BulletData(_bulletsSettings.GetBulletSettings(bulletType));
            bullet.InitMovement(_playerMovementModule.PositionPlayer.Value, _playerMovementModule.RotationPlayer.Value,
                (_playerMovementModule.RotationPlayer.Value * Vector3.up).normalized, Quaternion.identity);
            OnShot?.Invoke(bullet);
        }

        public void Dispose()
        {
            _playerInputControls?.Dispose();
        }
    }
}