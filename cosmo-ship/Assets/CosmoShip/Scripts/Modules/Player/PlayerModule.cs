using System;
using CosmoShip.Scripts.ClientServices.RXExtension;
using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using CosmoShip.Scripts.Models.BorderMap;
using CosmoShip.Scripts.Models.Player;
using CosmoShip.Scripts.Modules.Movements;
using CosmoShip.Scripts.ScriptableObjects.Player;
using CosmoShip.Scripts.Utils.Updatable;
using UnityEngine;

namespace CosmoShip.Scripts.Modules.Player
{
    public interface IPlayerModule
    {
        public IReadOnlyReactiveProperty<bool> OnActivePlayer { get; }
        public PlayerData PlayerData { get; }
        public void SpawnPlayer();
    }

    public class PlayerModule : IPlayerModule
    {        
        public PlayerData PlayerData => _playerData;
        
        public IReadOnlyReactiveProperty<bool> OnActivePlayer => _onActivePlayer;
        
        private ReactiveProperty<bool> _onActivePlayer = new ReactiveProperty<bool>();
        private PlayerData _playerData;
        private readonly PlayerSettings _playerSettings;
        
        public PlayerModule(PlayerSettings playerSettings, PlayerInputControls playerInputControls)
        {
            _playerSettings = playerSettings;
            Init();
        }

        private void Init()
        {
            _playerData = new PlayerData(_playerSettings);
            _playerData.OnDestroy(() =>
            {
                _onActivePlayer.Value = false;
            });
        }
        
        public void SpawnPlayer()
        {
            _onActivePlayer.Value = true;
        }
    }
}