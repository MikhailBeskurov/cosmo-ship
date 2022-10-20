using System;
using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using CosmoShip.Scripts.Models.Bullets;
using CosmoShip.Scripts.ScriptableObjects.Bullets;
using CosmoShip.Scripts.Utils.Updatable;

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

        public LaserWeaponModule(PlayerInputControls playerInputControls, BulletsSettings bulletsSettings, 
            IUpdateModule updateModule)
        {
            _updateModule = updateModule;
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
                OnShot?.Invoke(_bulletsSettings.GetBulletData(bulletType));
            }
        }

        public void Dispose()
        {
            _playerInputControls?.Dispose();
            _updateModule.RemoveAction(Updatable);
        }
    }
}