using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using CosmoShip.Scripts.Models.Movement;
using CosmoShip.Scripts.ScriptableObjects.Player;
using UnityEngine;
using UnityEngine.Events;

namespace CosmoShip.Scripts.Models.Player
{
    public class PlayerData : MovementData
    { 
        public readonly Sprite IconPlayer;
        public IReadOnlyReactiveProperty<int> HealtPoint => _healtPoint;

        private ReactiveProperty<int> _healtPoint = new ReactiveProperty<int>();
        private event UnityAction _onDestroy;
        
        public PlayerData(PlayerSettings playerSettings)
        {
            IconPlayer = playerSettings.IconPlayer;
            _healtPoint.Value = playerSettings.HealtPoint;
            InitSettingsMovement(playerSettings.MovementSettings);
        }
        
        public void OnDestroy(UnityAction onDestroy)
        {
            _onDestroy += onDestroy;
        }
        
        public void PutDamage(int damage)
        {
            _healtPoint.Value -= damage;
            if (_healtPoint.Value <= 0)
            {
                _healtPoint.Value = 0;
                _onDestroy?.Invoke();
            }
        }
    }
}