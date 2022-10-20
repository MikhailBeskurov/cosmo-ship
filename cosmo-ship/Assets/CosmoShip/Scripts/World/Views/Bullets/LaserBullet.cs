using System;
using CosmoShip.Scripts.Models.Bullets;
using CosmoShip.Scripts.Modules.Movements;
using UnityEngine;

namespace CosmoShip.Scripts.World.Views.Bullets
{
    public class LaserBullet : BaseBulletView
    {
        protected override IMovementModule _movementModule { get; set; }
        public override void Init(Action<GameObject, int> onTriggerEnter, BulletData bulletData, Action<BulletData> onDisable)
        {
            base.Init(onTriggerEnter, bulletData, onDisable);
            
            _movementModule = new ConstantMovement(bulletData.InitPosition, bulletData.InitRotation,bulletData.DirectionMove, 
                bulletData.MoveSpeedBullet, Vector3.zero, 0);
            
            _movementModule.Position.Subscribe(v =>
            {
                transform.position = v;
            });
            _movementModule.Rotation.Subscribe(v =>
            {
                transform.rotation = v;
            });
        }

        public override void UpdateView(float deltaTime)
        {
            base.UpdateView(deltaTime);
            _movementModule.Update(Time.deltaTime);
        }
    }
}