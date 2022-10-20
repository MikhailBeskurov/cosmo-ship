using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using CosmoShip.Scripts.ScriptableObjects.Player;
using UnityEngine;
using UnityEngine.Events;

namespace CosmoShip.Scripts.Models.Player
{
    public class PlayerData
    {
        public IReadOnlyReactiveProperty<int> HealtPoint => _healtPoint;
        public readonly Sprite IconPlayer;
        public readonly float SpeedMovement;
        public readonly float SpeedRotation;
        public readonly float InertiaVelocity;

        private ReactiveProperty<int> _healtPoint = new ReactiveProperty<int>();
        private event UnityAction _onDestroy;
        
        public PlayerData(PlayerSettings playerSettings)
        {
            IconPlayer = playerSettings.IconPlayer;
            SpeedMovement = playerSettings.SpeedMovement;
            SpeedRotation = playerSettings.SpeedRotation;
            InertiaVelocity = playerSettings.InertiaVelocity;
            _healtPoint.Value = playerSettings.HealtPoint;
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