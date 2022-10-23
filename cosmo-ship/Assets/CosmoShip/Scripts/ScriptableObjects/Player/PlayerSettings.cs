using CosmoShip.Scripts.Models.Movement;
using UnityEngine;

namespace CosmoShip.Scripts.ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Settings/PlayerSettings", order = 0)]
    public class PlayerSettings : ScriptableObject
    {
        public Sprite IconPlayer;
        public int HealtPoint = 1;
        public MovementSettings MovementSettings;
    }
}