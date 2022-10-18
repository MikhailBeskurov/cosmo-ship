using UnityEngine;

namespace CosmoShip.Scripts.Models.BorderMap
{
    public class BorderMapData
    {
        public BorderMapData(Rect rectBorderMap)
        {
            Widht = rectBorderMap.width;
            Height = rectBorderMap.height;
        }

        public readonly float Widht;
        public readonly float Height;
    }
}