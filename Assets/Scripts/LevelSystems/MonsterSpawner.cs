using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace LevelSystems
{
    public class MonsterSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyBase _enemy;
        [SerializeField] private EnemyConfig _enemyConfig;
        
        private float _spawnInterval;
        private int _maxMonsters;
        
        private List<EnemyBase> _spawnedMonsters;
        private EnemyFactory _enemyFactory;
        
        private void Start()
        {
            _spawnedMonsters = new List<EnemyBase>();
            _enemyFactory = new EnemyFactory(_enemyConfig);
        }
        
        public EnemyBase SpawnEnemy(EnemyType type, Vector3 position, Quaternion rotation)
        {
            Vector3 spawnPosition = position;
            EnemyBase enemy = _enemyFactory.CreateEnemy(type, spawnPosition, rotation, transform);

            if (!ReferenceEquals(enemy, null))
            {
                _spawnedMonsters.Add(enemy);
            }

            return enemy;
        }
    }
}
