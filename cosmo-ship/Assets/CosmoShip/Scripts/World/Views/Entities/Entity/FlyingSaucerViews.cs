using System;
using CosmoShip.Scripts.Models.Entities;
using CosmoShip.Scripts.Modules.Movements;
using UnityEngine;

namespace CosmoShip.Scripts.World.Views.Entities.Entity
{
    public class FlyingSaucerViews: BaseEntityView
    {
        private PathfinderMovement _pathfinderMovement;
        public override void Init(EntityData entityData)
        {
            base.Init(entityData);
            _pathfinderMovement = new PathfinderMovement(entityData, gameObject.GetHashCode());

            _pathfinderMovement.Position.Subscribe(v =>
            {
                EntityInfo.PositionTo(v);
                transform.position = v;
            });
            
            _pathfinderMovement.Rotation.Subscribe(v =>
            {
                transform.rotation = v;
            });
        }
        
        public override void UpdateView(float deltaTime)
        {
            base.UpdateView(deltaTime);
            _pathfinderMovement.Update(Time.deltaTime);
        }
    }
}