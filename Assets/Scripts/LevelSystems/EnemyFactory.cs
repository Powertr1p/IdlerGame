using Enemy;
using UnityEngine;

namespace LevelSystems
{
    public class EnemyFactory
    {
        private readonly EnemyConfig _config;
        
        public EnemyFactory(EnemyConfig config)
        {
            _config = config;
        }

        public EnemyBase CreateEnemy(EnemyType type, Vector3 position, Quaternion rotation, Transform parent)
        {
            EnemyBase prefab = _config.GetPrefab(type);

            if (ReferenceEquals(prefab, null))
            {
                Debug.LogError($"Enemy prefab for type {type} not found!");
                return null;
            }

            EnemyBase enemy = Object.Instantiate(prefab, position, rotation, parent);
            return enemy;
        }
    }
}