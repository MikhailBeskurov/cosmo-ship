using System;

namespace CosmoShip.Scripts.Models.Movement
{
    [Serializable]
    public class MovementSettings
    {
        public float SpeedRotation;
        public float SpeedMovement;
        public float SmoothVelocity;
        public float InertiaSpeed;
    }
}