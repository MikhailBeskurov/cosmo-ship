using System;
using CosmoShip.Scripts.ClientServices.RXExtension;
using CosmoShip.Scripts.ClientServices.RXExtension.Property;
using CosmoShip.Scripts.Modules.Player;
using CosmoShip.Scripts.UI.Core.Model;
using UnityEngine;

namespace CosmoShip.Scripts.UI.Models.PlayerInterface
{
    public class PlayerInterfaceModel : AbstractScreenModel
    {   
        public readonly IReadOnlyReactiveProperty<int> PlayerScore;
        public readonly IReadOnlyReactiveProperty<Vector2> PlayerCoordinates;
        public readonly IReadOnlyReactiveProperty<float> InstantSpeed;
        public readonly IReadOnlyReactiveProperty<float> PlayerRotationAngle;
        
        public PlayerInterfaceModel(IPlayerMovementModule playerMovementModule, 
            IPlayerScoreModule playerScoreModule)
        {
            PlayerScore = playerScoreModule.ScorePlayer;
            PlayerCoordinates = playerMovementModule.PositionPlayer;
            InstantSpeed = playerMovementModule.InstantSpeed;
            PlayerRotationAngle = playerMovementModule.AngleRotation;
        }
    }
}