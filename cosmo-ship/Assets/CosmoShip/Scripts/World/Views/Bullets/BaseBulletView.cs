using System;
using CosmoShip.Scripts.Models;
using CosmoShip.Scripts.Models.Bullets;
using CosmoShip.Scripts.Modules.Movements;
using UnityEngine;

namespace CosmoShip.Scripts.World.Views.Bullets
{
    public class BaseBulletView : MonoBehaviour
    {
        public BulletData BulletData => _bulletData;
        
        private Action<GameObject, int> _onTriggerEnter;
        private BulletData _bulletData;
        
        private float _timeLifeSeconds = 3f;
        private DateTime _spawnDateTime;
        protected Action<BulletData> _onDisable;

        protected virtual BaseMovementModule _movementModule { get; set; }
        
        public virtual void Init(Action<GameObject, int> onTriggerEnter, BulletData bulletData, 
            Action<BulletData> onDisable)
        {
            _onDisable = onDisable;
            _bulletData = bulletData;
            _onTriggerEnter = onTriggerEnter;
            _spawnDateTime = DateTime.Now;
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
            if (DateTime.Now.Subtract(_spawnDateTime).Seconds > _timeLifeSeconds)
            {
                _onDisable?.Invoke(BulletData);
            }
        }
        
        public virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag(EntitiesTag.Entity.ToString()))
            {
                _onTriggerEnter?.Invoke(col.gameObject, _bulletData.Damage);
            }
        }
    }
}