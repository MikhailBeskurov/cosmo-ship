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
                UpdatePositionRotation(v,_movementModule.Rotation.Value);
            });
            
            _movementModule.Rotation.Subscribe(v =>
            {
                UpdatePositionRotation(_movementModule.Position.Value,v);
            });
        }

        private void UpdatePositionRotation(Vector2 pos, Quaternion rot)
        {
            transform.position = pos;
            transform.rotation = rot;
        }

        public override void UpdateView(float deltaTime)
        {
            _movementModule.Update(Time.deltaTime);
        }
    }
}