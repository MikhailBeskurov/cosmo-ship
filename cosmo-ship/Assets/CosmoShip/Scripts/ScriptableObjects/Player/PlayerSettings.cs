using UnityEngine;

namespace CosmoShip.Scripts.ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Settings/PlayerSettings", order = 0)]
    public class PlayerSettings : ScriptableObject
    {
        public Sprite IconPlayer;
        public int HealtPoint = 1;
        public float SpeedMovement = 20;
        public float SpeedRotation = 20;
        public float InertiaVelocity = 0.6f;
    }
}