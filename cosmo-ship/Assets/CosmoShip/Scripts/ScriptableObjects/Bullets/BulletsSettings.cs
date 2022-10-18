﻿using System;
using System.Collections.Generic;
using CosmoShip.Scripts.Models.Bullets;
using CosmoShip.Scripts.World.Views.Bullets;
using UnityEngine;

namespace CosmoShip.Scripts.ScriptableObjects.Bullets
{
    [UnityEngine.CreateAssetMenu(fileName = "BulletsSettings", menuName = "Settings/BulletsSettings", order = 0)]
    public class BulletsSettings : UnityEngine.ScriptableObject
    {
        [SerializeField] private List<BulletSettingsData> _bullets = new List<BulletSettingsData>();
        
        public BulletData GetBulletData(BulletTypes bulletType)
        {
            return new BulletData(_bullets.Find(v => v.BulletType == bulletType));
        }
    }
    
    [Serializable]
    public class BulletSettingsData
    {
        public BaseBulletView BulletView;
        public BulletTypes BulletType;
        public int Damage = 1;
        public float MoveSpeedBullet = 5f;
    }
}