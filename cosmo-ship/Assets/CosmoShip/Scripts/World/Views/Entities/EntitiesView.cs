using System.Collections.Generic;
using System.Linq;
using CosmoShip.Scripts.Models.Entities;
using CosmoShip.Scripts.World.Core.View;
using CosmoShip.Scripts.World.Models.Entities;
using CosmoShip.Scripts.World.Views.Entities.Entity;
using UnityEngine;

namespace CosmoShip.Scripts.World.Views.Entities
{
    public class EntitiesView : SimpleWorldView<EntitiesModel>
    {
        private List<BaseEntityView> _entityViews = new List<BaseEntityView>();
        private Queue<BaseEntityView> _queueEntityView = new Queue<BaseEntityView>();
        
        private EntitiesModel _model;
        
        public override void Bind(EntitiesModel model)
        {
            _model = model;
            model.Entities.ObserveAdd().Subscribe(v =>
            {
                AddEntity(v);
            });
            model.Entities.ObserveRemove().Subscribe(v =>
            {
                RemoveEntity(v);
            });
            model.UpdateModule.AddAction(Updatable);
        }

        public override void Hide()
        {
            Dispose();
            base.Hide();
        }

        private void Updatable(float deltaTime)
        {
            foreach (var entityView in _queueEntityView)
            {
                entityView.UpdateView(deltaTime);
            }
        }

        private void AddEntity(EntityData entityData)
        {
            var entity = PullEntityView(entityData);
            entity.Init(entityData);
            entity.Show();
            _queueEntityView.Enqueue(entity);
        }
        
        private void RemoveEntity(EntityData entityData)
        {
            var entity = _entityViews.Find(v => v.EntityInfo == entityData);
            if (entity != null)
            {
                _queueEntityView = new Queue<BaseEntityView>(_queueEntityView.Where(v => v != entity));
                entity.Hide();
            }
        }

        private BaseEntityView PullEntityView(EntityData entityData)
        {
            var entity = _entityViews.Find(v => !v.gameObject.activeSelf 
                                                && v.EntityInfo.TypeEntity == entityData.TypeEntity);
            if (entity == null)
            {
                entity = Instantiate(_model.GetEntityObject(entityData.TypeEntity), entityData.CurrentPosition,
                    Quaternion.identity);
                _entityViews.Add(entity);
            }
            
            return entity;
        }
        
        private void Dispose()
        {
            _model.UpdateModule.RemoveAction(Updatable);
        }
    }
}
