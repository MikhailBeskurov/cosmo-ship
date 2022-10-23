using System;
using CosmoShip.Scripts.Models;
using CosmoShip.Scripts.Models.Entities;
using CosmoShip.Scripts.Modules.Movements;
using Unity.VisualScripting;
using UnityEngine;

namespace CosmoShip.Scripts.World.Views.Entities.Entity
{
    public abstract class BaseEntityView : MonoBehaviour
    {
        public EntityData EntityInfo => _entityInfo;

        protected BaseMovementModule MovementModule;
        
        private EntityData _entityInfo;
        
        public virtual void Init(EntityData entityData)
        {
            _entityInfo = entityData;
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