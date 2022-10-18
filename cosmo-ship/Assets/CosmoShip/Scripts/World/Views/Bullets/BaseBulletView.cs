using System;
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
        
        protected virtual IMovementModule _movementModule { get; set; }
        
        public virtual void Init(Action<GameObject, int> onTriggerEnter, BulletData bulletData)
        {
            _bulletData = bulletData;
            _onTriggerEnter = onTriggerEnter;
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
        
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Entity"))
            {
                _onTriggerEnter?.Invoke(col.gameObject, _bulletData.Damage);
            }
        }
    }
}