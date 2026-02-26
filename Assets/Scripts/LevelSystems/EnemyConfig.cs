using Enemy;
using UnityEngine;

namespace LevelSystems
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private EnemyPrefab[] _enemies;
        
        public EnemyBase GetPrefab(EnemyType type)
        {
            foreach (var enemy in _enemies)
            {
                if (enemy.Type == type)
                {
                    return enemy.Prefab;
                }
            }

            return null;
        }
        
        [System.Serializable]
        private class EnemyPrefab
        {
            public EnemyType Type;
            public EnemyBase Prefab;
        }
    }
}