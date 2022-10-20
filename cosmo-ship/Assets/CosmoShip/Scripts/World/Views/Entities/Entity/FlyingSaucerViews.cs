using System;
using CosmoShip.Scripts.Models.Entities;
using CosmoShip.Scripts.Modules.Movements;
using UnityEngine;

namespace CosmoShip.Scripts.World.Views.Entities.Entity
{
    public class FlyingSaucerViews: BaseEntityView
    {
        protected override IMovementModule _movementModule { get; set; }

        public override void Init(EntityData entityData)
        {
            base.Init(entityData);
            _movementModule = new PathfinderMovement(entityData.CurrentPosition, Quaternion.Euler(Vector2.up),
                entityData.DirectionMove, entityData.MoveSpeedEntity, gameObject.GetHashCode());

            _movementModule.Position.Subscribe(v =>
            {
                EntityInfo.SetCurrentPosition(v);
                transform.position = v;
            });
            _movementModule.Rotation.Subscribe(v =>
            {
                transform.rotation = v;
            });
            _movementModule.TeleportationToPoint(entityData.CurrentPosition);
        }
        
        public override void UpdateView(float deltaTime)
        {
            base.UpdateView(deltaTime);
            _movementModule.Update(Time.deltaTime);
        }
    }
}