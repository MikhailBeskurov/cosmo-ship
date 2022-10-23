using System;
using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using CosmoShip.Scripts.Models.Bullets;
using CosmoShip.Scripts.Models.Movement;
using CosmoShip.Scripts.ScriptableObjects.Bullets;
using CosmoShip.Scripts.Utils.Updatable;
using UnityEngine;

namespace CosmoShip.Scripts.Modules.Player.Weapons
{
    public interface ILaserWeaponModule
    {
        public Action<BulletData> OnShot { get; set; }
        public IReadOnlyReactiveProperty<int> LaserAttempts { get; }
        public IReadOnlyReactiveProperty<float> SecondsToRecoverLaser { get; }
    }

    public class LaserWeaponModule : ILaserWeaponModule, IDisposable
    {
        public Action<BulletData> OnShot { get; set; }

        public IReadOnlyReactiveProperty<int> LaserAttempts => _laserAttempts;
        public IReadOnlyReactiveProperty<float> SecondsToRecoverLaser => _secondsToRecoverLaser;
        
        private ReactiveProperty<int> _laserAttempts = new ReactiveProperty<int>();
        private ReactiveProperty<float> _secondsToRecoverLaser = new ReactiveProperty<float>();
        
        private PlayerInputControls _playerInputControls;
        private BulletsSettings _bulletsSettings;
        
        private DateTime _lastLaserShot;
        private float _laserResetDuratuion = 5f;
        private IUpdateModule _updateModule;
        private readonly IPlayerMovementModule _playerMovementModule;

        public LaserWeaponModule(PlayerInputControls playerInputControls, BulletsSettings bulletsSettings, 
            IUpdateModule updateModule, IPlayerMovementModule playerMovementModule)
        {
            _updateModule = updateModule;
            _playerMovementModule = playerMovementModule;
            _bulletsSettings = bulletsSettings;
            _playerInputControls = playerInputControls;
            updateModule.AddAction(Updatable);
            Init();
        }

        
        private void Init()
        {
            _lastLaserShot = DateTime.Now;
            BindLaserShoot();
        }

        private void BindLaserShoot()
        {
            _playerInputControls.SpaceShip.LaserShoot.performed += context =>
            {
                ShotWeapon(BulletTypes.Laser);
            };
        }

        private void Updatable(float deltaTime)
        {
            float timeReset = _laserResetDuratuion
                              - Convert.ToSingle(DateTime.Now.Subtract(_lastLaserShot)
                                  .TotalMilliseconds / 1000);
            _secondsToRecoverLaser.Value = timeReset;
            if (timeReset <= 0)
            {
                _laserAttempts.Value++;
                _lastLaserShot = DateTime.Now;
            }
        }

        private void ShotWeapon(BulletTypes bulletType)
        {
            if (_laserAttempts.Value > 0)
            {
                _laserAttempts.Value--;
                var bullet = new BulletData(_bulletsSettings.GetBulletSettings(bulletType));
                bullet.InitMovement(_playerMovementModule.PositionPlayer.Value, _playerMovementModule.RotationPlayer.Value,
                    (_playerMovementModule.RotationPlayer.Value * Vector3.up).normalized, Quaternion.identity);
                OnShot?.Invoke(bullet);
            }
        }

        public void Dispose()
        {
            _playerInputControls?.Dispose();
            _updateModule.RemoveAction(Updatable);
        }
    }
}