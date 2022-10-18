using System;
using CosmoShip.Scripts.ClientServices.RXExtension;
using CosmoShip.Scripts.Models.Entities;
using CosmoShip.Scripts.Modules.Movements;
using Unity.VisualScripting;
using UnityEngine;

namespace CosmoShip.Scripts.World.Views.Entities.Entity
{
    public class AsteroidView: BaseEntityView, IDisposable
    {
        protected override IMovementModule _movementModule { get; set; }
        private DisposableList _disposableList = new DisposableList();
        
        public override void Init(EntityData entityData)
        {
            base.Init(entityData);
            _movementModule = new ConstantMovement(entityData.CurrentPosition, Quaternion.identity, entityData.DirectionMove.Value, 
                entityData.MoveSpeedEntity, new Vector3(0,0,-1),
                entityData.RotationSpeedEntity);
            
            _movementModule.Position.Subscribe(PositionChange).AddDispose(_disposableList);
            _movementModule.Rotation.Subscribe(RotationChange).AddDispose(_disposableList);
        }

        public override void UpdateView(float deltaTime)
        {
            base.UpdateView(deltaTime);
            _movementModule.Update(deltaTime);
        }

        private void PositionChange(Vector2 newPosition)
        {
            EntityInfo.SetCurrentPosition(newPosition);
            transform.position = newPosition;
        }
        
        private void RotationChange(Quaternion newRotation)
        {
            transform.rotation = newRotation;
        }

        public void Dispose()
        {
            Debug.Log("Dispose");
            _disposableList.Dispose();
        }
    }
}