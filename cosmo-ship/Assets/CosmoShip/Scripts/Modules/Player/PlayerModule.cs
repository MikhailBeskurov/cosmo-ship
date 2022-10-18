using System;
using CosmoShip.Scripts.ClientServices.RXExtension.Property;
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
        public IReadOnlyReactiveProperty<Vector2> PositionPlayer { get; }
        public IReadOnlyReactiveProperty<Quaternion> RotationPlayer { get; }
    }

    public class PlayerModule : IPlayerModule, IDisposable
    {        
        public PlayerData PlayerData => _playerData;
        public IReadOnlyReactiveProperty<bool> OnActivePlayer => _onActivePlayer;
        public IReadOnlyReactiveProperty<Vector2> PositionPlayer => _movementModule.Position;
        public IReadOnlyReactiveProperty<Quaternion> RotationPlayer => _movementModule.Rotation;

        private PlayerData _playerData;
        private ReactiveProperty<Vector2> _positionPlayer = new ReactiveProperty<Vector2>();
        private ReactiveProperty<Quaternion> _rotationPlayer = new ReactiveProperty<Quaternion>();
        private ReactiveProperty<bool> _onActivePlayer = new ReactiveProperty<bool>();
        
        private readonly PlayerInputControls _playerInputControls;
        private readonly PlayerSettings _playerSettings;
        private IMovementModule _movementModule;
        private IUpdateModule _updateModule;

        public PlayerModule(PlayerSettings playerSettings, PlayerInputControls playerInputControls, IUpdateModule updateModule)
        {
            _updateModule = updateModule;
            _playerInputControls = playerInputControls;
            _playerInputControls.Enable();
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
            
            _movementModule = new InputMovement(_positionPlayer, _rotationPlayer, _playerData.SpeedMovement,
                _playerData.SpeedRotation,_playerData.InertiaVelocity);
            
            _updateModule.AddAction(_movementModule.Update);
            
            BindPositionInput();
            BindRotationInput();
            
            _playerInputControls.SpaceShip.Spawn.performed += context => 
            { 
                _onActivePlayer.Value = true;
            };
        }

        private void BindPositionInput()
        {
            _playerInputControls.SpaceShip.Move.performed += context =>
            {
                _positionPlayer.Value = context.ReadValue<Vector2>();
            };
            _playerInputControls.SpaceShip.Move.canceled += context =>
            {
                _positionPlayer.Value = Vector2.zero;
            };
        }
        
        private void BindRotationInput()
        {
            _playerInputControls.SpaceShip.Rotation.performed += context =>
            {
                _rotationPlayer.Value = Quaternion.Euler(new Vector3(0,0,context.ReadValue<Vector2>().x));
            };
            _playerInputControls.SpaceShip.Rotation.canceled += context =>
            {
                _rotationPlayer.Value = Quaternion.identity;
            };
        }

        public void Dispose()
        {
            _playerInputControls.Disable();
        }
    }
}