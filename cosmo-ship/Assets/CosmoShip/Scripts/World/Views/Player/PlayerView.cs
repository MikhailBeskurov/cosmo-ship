using System;
using System.Collections.Generic;
using CosmoShip.Scripts.ClientServices.RXExtension;
using CosmoShip.Scripts.Models.Bullets;
using CosmoShip.Scripts.World.Core.View;
using CosmoShip.Scripts.World.Models.Player;
using CosmoShip.Scripts.World.Views.Bullets;
using CosmoShip.Scripts.World.Views.Entities.Entity;
using UnityEngine;

namespace CosmoShip.Scripts.World.Views.Player
{
    public class PlayerView : SimpleWorldView<PlayerModel>
    {
        [SerializeField] private SpriteRenderer _iconPlayer;
     
        private PlayerModel _model;
        private DisposableList _disposableList = new DisposableList();
        private List<BaseBulletView> _bulletViews = new List<BaseBulletView>();

        public override void Bind(PlayerModel model)
        {
            _model = model;
            _iconPlayer.sprite = _model.PlayerInit.IconPlayer;
            _model.OnActivePlayer.Subscribe(v =>
            {
                if (v)
                {
                    Show();
                }
                else
                {
                    Hide();
                }
            });
        }

        public override void Show()
        {
            if(!IsShown){
                Init(); 
            }
            base.Show();
        }

        public override void Hide()
        {
            Dispose();
            base.Hide();
        }
        
        private void Init()
        {
            _model.PositionPlayer.Subscribe(v =>
            {
                transform.position = v;
            }).AddDispose(_disposableList);
            
            _model.RotationPlayer.Subscribe(v =>
            {
                transform.rotation = v;
            }).AddDispose(_disposableList);
            
            _model.OnShot += Shooting;
        }

        private void Shooting(BulletData bulletData)
        {     
            var bullet = PullBulletView(bulletData);
            bulletData.Init(transform.position, transform.rotation, 
                (transform.rotation * Vector3.up).normalized);
            bullet.Init(DamageTaken, bulletData);
            bullet.Show();
        }

        private void DamageTaken(GameObject obj, int Damage)
        {
            var entityView = obj.GetComponent<BaseEntityView>();
            if (entityView)
            {
                _model.DamageTaken(entityView.EntityInfo, Damage);
            }
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Entity"))
            {
                var entityView = col.GetComponent<BaseEntityView>();
                if (entityView)
                {
                    _model.DamageGiven(entityView.EntityInfo);
                }
            }
        }

        private BaseBulletView PullBulletView(BulletData bulletData)
        {
            var bullet = _bulletViews.Find(v => !v.gameObject.activeSelf 
                                                && v.BulletData.BulletType == bulletData.BulletType);
            if (bullet == null)
            {
                bullet = Instantiate(bulletData.BulletView);
                _bulletViews.Add(bullet);
            }
            return bullet;
        }

        private void Dispose()
        {
            _model.OnShot -= Shooting;
            _disposableList.Dispose();
        }
    }
}