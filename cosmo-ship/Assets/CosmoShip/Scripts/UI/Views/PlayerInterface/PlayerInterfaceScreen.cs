using CosmoShip.Scripts.UI.Models.PlayerInterface;
using GethererHeroes.Scripts.UI.Core.View;
using TMPro;
using UnityEngine;

namespace CosmoShip.Scripts.UI.Views.PlayerInterface
{
    public class PlayerInterfaceScreen : SimpleScreen<PlayerInterfaceModel>
    {
        [SerializeField] private TMP_Text _playerScore;
        [SerializeField] private TMP_Text _playerСoordinates;
        [SerializeField] private TMP_Text _instantSpeed;
        [SerializeField] private TMP_Text _playerRotationAngle;
        
        public override void Bind(PlayerInterfaceModel model)
        {
            model.InstantSpeed.Subscribe(v =>
            {
                _instantSpeed.text = $"Speed: {(int)v}";
            });
            model.PlayerCoordinates.Subscribe(v =>
            {
                _playerСoordinates.text = $"Coordinates: ({(int)v.x}:{(int)v.y})";
            });
            model.PlayerRotationAngle.Subscribe(v =>
            {
                _playerRotationAngle.text = $"Rotation: {(int)v}";
            });
            model.PlayerScore.Subscribe(v =>
            {
                _playerScore.text = $"Score: {v}";
            });
        }
    }
}