using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Object = UnityEngine.Object;

namespace CosmoShip.Scripts.ClientServices
{
    public interface IGameResourcesManager
    {
        Sprite GetSprite(string name);
        T GetPrefab<T>(string name) where T : Component;
    }

    public class GameResourcesManager : IGameResourcesManager
    {
        private Dictionary<string, List<SpriteAtlas>> _spriteGroups = new Dictionary<string, List<SpriteAtlas>>();
        private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
        private Dictionary<string, Component> _prefabs = new Dictionary<string, Component>();

        private List<SpriteAtlas> _atlases = new List<SpriteAtlas>();

        public void LoadGroups(string[] atlasesArray, Action afterLoading)
        {
            foreach (var atlasName in atlasesArray)
            {
                var spriteAtlases = Resources.Load<SpriteAtlas>(atlasName);
                _atlases.Add(spriteAtlases);
            
                _spriteGroups.Add(atlasName, _atlases);
            }
            
            afterLoading?.Invoke();
        }
        
        public T GetPrefab<T>(string name) where T : Component
        {
            if (_prefabs.TryGetValue(name, out var value))
            {
                return (T)value;
            }
            
            var prefab = Resources.Load<T>(name);
            _prefabs.Add(name, prefab);
            
            return prefab;
        }

        public Sprite GetSprite(string name)
        {
            if (_sprites.TryGetValue(name, out var sprite))
            {
                return sprite;
            }
            
            foreach (var spriteAtlas in _spriteGroups)
            {
                foreach (var atlases in spriteAtlas.Value)
                {
                    var correctSprite = atlases.GetSprite(name);
                    if (correctSprite != null)
                    {
                        _sprites.Add(name, correctSprite);
                        return correctSprite;
                    }
                }
            }

            return null;
        }
    }
}