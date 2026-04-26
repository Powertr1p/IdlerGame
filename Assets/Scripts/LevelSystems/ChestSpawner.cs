using DefaultNamespace;
using Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace LevelSystems
{
    [RequireComponent(typeof(Collider))]
    public class ChestSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private EnemyType _enemyType = EnemyType.Zombie;
        [SerializeField] private Transform _player;
        [SerializeField] private int _minEnemies = 3;
        [SerializeField] private int _maxEnemies = 5;
        [SerializeField] private float _spawnRadius = 3f;

        private EnemyFactory _factory;
        private bool _spawned;

        private void Awake()
        {
            _factory = new EnemyFactory(_enemyConfig);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_spawned) return;
            if (!other.TryGetComponent(out IExitable _)) return;

            int count = Random.Range(_minEnemies, _maxEnemies + 1);
            for (int i = 0; i < count; i++)
            {
                Vector3 position = GetSpawnPosition();
                Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                EnemyBase enemy = _factory.CreateEnemy(_enemyType, position, rotation, null);

                if (ReferenceEquals(enemy, null)) continue;
                if (ReferenceEquals(_player, null)) continue;

                enemy.SetTarget(_player);
            }

            _spawned = true;
        }

        private Vector3 GetSpawnPosition()
        {
            Vector2 offset = Random.insideUnitCircle * _spawnRadius;
            Vector3 candidate = transform.position + new Vector3(offset.x, 0f, offset.y);

            if (NavMesh.SamplePosition(candidate, out NavMeshHit hit, _spawnRadius, NavMesh.AllAreas))
                return hit.position;

            return transform.position;
        }
    }
}
