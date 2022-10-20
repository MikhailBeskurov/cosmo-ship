using UnityEngine;

namespace CosmoShip.Scripts.Models.BorderMap
{
    public class BorderMapData
    {
        public BorderMapData(float camOrthographicSize)
        { 
            Height = Camera.main.orthographicSize * 2.0f; 
            Widht = Height * Screen.width / Screen.height;
        }

        public readonly float Widht;
        public readonly float Height;
    }
}