using System;
using CosmoShip.Scripts.Models.Entities;
using CosmoShip.Scripts.Models.Player;
using CosmoShip.Scripts.Modules.Entities;
using UnityEngine;

namespace CosmoShip.Scripts.Modules.Damage
{
    public interface IDamageModule
    {
        public void DamageTaken(EntityData entityData, int Damage);
        public void DamageTaken(PlayerData playerData, int Damage);
    }

    public class DamageModule : IDamageModule
    {
        public DamageModule()
        {
           
        }

        public void DamageTaken(EntityData entityData, int Damage)
        {
            entityData.PutDamage(Damage);
        }
        
        public void DamageTaken(PlayerData playerData, int Damage)
        {
            playerData.PutDamage(Damage);
        }
    }
}