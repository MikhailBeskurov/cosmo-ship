using System;
using CosmoShip.Scripts.ClientServices.RXExtension;
using CosmoShip.Scripts.Models.Entities;
using CosmoShip.Scripts.Modules.Movements;
using Unity.VisualScripting;
using UnityEngine;

namespace CosmoShip.Scripts.World.Views.Entities.Entity
{
    public class AsteroidView: BaseEntityView
    {
        protected override IMovementModule _movementModule { get; set; }
        
        public override void Init(EntityData entityData)
        {
            base.Init(entityData);
            _movementModule = new ConstantMovement(entityData.CurrentPosition, Quaternion.identity, entityData.DirectionMove.Value, 
                entityData.MoveSpeedEntity, new Vector3(0,0,-1),
                entityData.RotationSpeedEntity);
            
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
            _movementModule.Update(deltaTime);
        }
    }
}