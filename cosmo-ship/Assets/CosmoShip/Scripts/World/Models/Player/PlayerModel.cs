using System;
using CosmoShip.Scripts.ClientServices.RXExtension;
using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using CosmoShip.Scripts.Models.Bullets;
using CosmoShip.Scripts.Models.Entities;
using CosmoShip.Scripts.Models.Player;
using CosmoShip.Scripts.Modules.Damage;
using CosmoShip.Scripts.Modules.Player;
using CosmoShip.Scripts.Modules.Player.Weapons;
using CosmoShip.Scripts.Utils.Updatable;
using CosmoShip.Scripts.World.Core.Model;
using UnityEngine;

namespace CosmoShip.Scripts.World.Models.Player
{
    public class PlayerModel : AbstractWorldViewModel, IDisposable
    {
        public Action<BulletData> OnShot;
        
        public IReadOnlyReactiveProperty<bool> OnActivePlayer;
        public IReadOnlyReactiveProperty<Vector2> PositionPlayer;
        public IReadOnlyReactiveProperty<Quaternion> RotationPlayer;
        public readonly PlayerData PlayerInit;
        public readonly IUpdateModule UpdateModule;
        
        private IDamageModule _damageModule;
        private ILaserWeaponModule _laserWeaponModule;
        private IBlasterWeaponModule _blasterWeaponModule;
        
        private DisposableList _disposableList = new DisposableList();

        public PlayerModel(IPlayerModule playerModule, IPlayerMovementModule playerMovementModule, ILaserWeaponModule laserWeaponModule,
            IBlasterWeaponModule blasterWeaponModule, IDamageModule damageModule, IUpdateModule updateModule)
        {
            _blasterWeaponModule = blasterWeaponModule;
            _laserWeaponModule = laserWeaponModule;
            UpdateModule = updateModule;
            _damageModule = damageModule;
            PlayerInit = playerModule.PlayerData;
            PositionPlayer = playerMovementModule.PositionPlayer;
            RotationPlayer = playerMovementModule.RotationPlayer;
            OnActivePlayer = playerModule.OnActivePlayer;
            
            _laserWeaponModule.OnShot += Shooting;
            _blasterWeaponModule.OnShot += Shooting;
        }
        
        public void DamageTaken(EntityData entityData, int Damage)
        {
            _damageModule.DamageTaken(entityData, Damage);
        }
        
        public void DamageGiven(EntityData entityData)
        {
            _damageModule.DamageTaken(PlayerInit, entityData.Damage);
        }
        
        private void Shooting(BulletData bulletData)
        {
            OnShot?.Invoke(bulletData);
        }

        public void Dispose()
        {          
            _laserWeaponModule.OnShot -= Shooting;
            _blasterWeaponModule.OnShot -= Shooting;
            _disposableList.Dispose();
        }
    }
}