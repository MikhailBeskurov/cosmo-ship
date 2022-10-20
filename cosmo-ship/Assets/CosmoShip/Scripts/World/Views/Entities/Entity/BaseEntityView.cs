using System;
using CosmoShip.Scripts.Models.Entities;
using CosmoShip.Scripts.Modules.Movements;
using Unity.VisualScripting;
using UnityEngine;

namespace CosmoShip.Scripts.World.Views.Entities.Entity
{
    public class BaseEntityView : MonoBehaviour
    {
        public EntityData EntityInfo { get; private set; }
        protected virtual IMovementModule _movementModule { get; set; }

        public virtual void Init(EntityData entityData)
        {
            EntityInfo = entityData;
        }
        
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
        
        public virtual void Hide()
        {
           gameObject.SetActive(false);
        }
        
        public virtual void UpdateView(float deltaTime)
        {
            
        }
    }
}