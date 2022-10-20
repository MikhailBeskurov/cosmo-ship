using System;
using CosmoShip.Scripts.ClientServices.RXExtension;
using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using CosmoShip.Scripts.Models.BorderMap;
using CosmoShip.Scripts.Modules.Movements;
using CosmoShip.Scripts.Utils.Updatable;
using UnityEngine;

namespace CosmoShip.Scripts.Modules.Player
{
    public interface IPlayerMovementModule
    {
        public IReadOnlyReactiveProperty<Vector2> PositionPlayer { get; }
        public IReadOnlyReactiveProperty<Quaternion> RotationPlayer { get; }
        public IReadOnlyReactiveProperty<float> InstantSpeed { get; }
        public IReadOnlyReactiveProperty<float> AngleRotation { get; }
    }

    public class PlayerMovementModule : IPlayerMovementModule, IDisposable
    {
        public IReadOnlyReactiveProperty<Vector2> PositionPlayer => _movementModule.Position;
        public IReadOnlyReactiveProperty<Quaternion> RotationPlayer => _movementModule.Rotation;
        public IReadOnlyReactiveProperty<float> InstantSpeed => _movementModule.InstantSpeed;
        public IReadOnlyReactiveProperty<float> AngleRotation => _movementModule.AngleRotation;
        
        private ReactiveProperty<Vector2> _positionVelocity = new ReactiveProperty<Vector2>();
        private ReactiveProperty<Quaternion> _rotationVelocity = new ReactiveProperty<Quaternion>();

        private readonly PlayerInputControls _playerInputControls;
        private IMovementModule _movementModule;
        private IUpdateModule _updateModule;
        private IPlayerModule _playerModule;
        private BorderMapData _borderMapData;
        
        private DisposableList _disposableList = new DisposableList();
        
        public PlayerMovementModule(IPlayerModule playerModule, PlayerInputControls playerInputControls, 
            IUpdateModule updateModule, BorderMapData borderMapData)
        {
            _playerModule = playerModule;
            _borderMapData = borderMapData;
            _updateModule = updateModule;
            _playerInputControls = playerInputControls;
            _playerInputControls.Enable();
            Init();
        }
        
        private void Init()
        {
            _movementModule = new InputMovement(_positionVelocity, _rotationVelocity, _playerModule.PlayerData.SpeedMovement,
                _playerModule.PlayerData.SpeedRotation,_playerModule.PlayerData.InertiaVelocity);
            
            _movementModule.Position.Subscribe(ChechBorderMap).AddDispose(_disposableList);

            _updateModule.AddAction(_movementModule.Update);
            
            BindPositionInput();
            BindRotationInput();
        }
        
        private void BindPositionInput()
        {
            _playerInputControls.SpaceShip.Move.performed += context =>
            {
                _positionVelocity.Value = context.ReadValue<Vector2>();
            };
            _playerInputControls.SpaceShip.Move.canceled += context =>
            {
                _positionVelocity.Value = Vector2.zero;
            };
        }
        
        private void BindRotationInput()
        {
            _playerInputControls.SpaceShip.Rotation.performed += context =>
            {
                _rotationVelocity.Value = Quaternion.Euler(new Vector3(0,0,context.ReadValue<Vector2>().x));
            };
            _playerInputControls.SpaceShip.Rotation.canceled += context =>
            {
                _rotationVelocity.Value = Quaternion.identity;
            };
        }
        
        private void ChechBorderMap(Vector2 position)
        {
            Vector2 newPosition = position;
            float borderWidht = _borderMapData.Widht / 2;
            float borderHeight = _borderMapData.Height / 2;
            
            if (position.x > borderWidht)
            {
                newPosition = new Vector2(-borderWidht, newPosition.y);
            }
            else if(position.x < -borderWidht)
            {
                newPosition = new Vector2(borderWidht, newPosition.y);
            }
            
            if (position.y > borderHeight)
            {
                newPosition = new Vector2(newPosition.x,-borderHeight);
            }
            else if(position.y < -borderHeight)
            {
                newPosition = new Vector2(newPosition.x,borderHeight);
            }

            if (newPosition != position)
            {
                _movementModule.TeleportationToPoint(newPosition);
            }
        }
        
        public void Dispose()
        {
            _updateModule.RemoveAction(_movementModule.Update);
            _playerInputControls.Disable();
            _disposableList.Dispose();
        }
    }
}