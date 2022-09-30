using UnityEngine;

namespace CosmoShip.Scripts.ClientServices
{
    public interface ISpriteProvider
    {
        
    }
    
    public static class SpriteProvider
    {
        private static IGameResourcesManager _gameResourcesManager;

        public static void Init(IGameResourcesManager gameResourcesManager)
        {
            _gameResourcesManager = gameResourcesManager;
        }

        public static Sprite GetSprite (this string value)
        {
            return _gameResourcesManager.GetSprite(value);
        }
    }
}