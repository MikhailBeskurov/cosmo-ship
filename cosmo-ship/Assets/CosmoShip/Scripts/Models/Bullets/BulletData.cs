using CosmoShip.Scripts.ScriptableObjects.Bullets;
using CosmoShip.Scripts.World.Views.Bullets;
using UnityEngine;

namespace CosmoShip.Scripts.Models.Bullets
{
    public class BulletData
    {
        public readonly BaseBulletView BulletView;
        public readonly BulletTypes BulletType;
        public readonly int Damage;
        public readonly float MoveSpeedBullet;
        
        public Vector2 InitPosition => _initPosition;
        public Quaternion InitRotation => _initRotation;
        public Vector2 DirectionMove => _directionMove;
        
        private Vector2 _initPosition;
        private Quaternion _initRotation;
        private Vector2 _directionMove;
        
        public BulletData(BulletSettingsData bulletsSettings)
        {
            BulletView = bulletsSettings.BulletView;
            BulletType = bulletsSettings.BulletType;
            Damage = bulletsSettings.Damage;
            MoveSpeedBullet = bulletsSettings.MoveSpeedBullet;
        }
        
        public void Init(Vector2 initPosition, Quaternion initRotation, Vector2 directionMove)
        {
            _initPosition = initPosition;
            _initRotation = initRotation;
            _directionMove = directionMove;
        }
    }
}