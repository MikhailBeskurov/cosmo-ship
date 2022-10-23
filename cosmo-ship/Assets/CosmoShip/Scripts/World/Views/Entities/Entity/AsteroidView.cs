using CosmoShip.Scripts.Models.Entities;
using CosmoShip.Scripts.Modules.Movements;

namespace CosmoShip.Scripts.World.Views.Entities.Entity
{
    public class AsteroidView: BaseEntityView
    {
        public override void Init(EntityData entityData)
        {
            base.Init(entityData);

            MovementModule = new ConstantMovement(entityData);
            
            MovementModule.Position.Subscribe(v =>
            {
                EntityInfo.PositionTo(v);
                transform.position = v;
            });
            
            MovementModule.Rotation.Subscribe(v =>
            {
                transform.rotation = v;
            });
        }

        public override void UpdateView(float deltaTime)
        {
            base.UpdateView(deltaTime);
            MovementModule.Update(deltaTime);
        }
    }
}