using System.Collections.Generic;
using ShareComponents;
using UnityEngine;

namespace PlayerWeapon
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private Transform _weaponPivot;
        
        [Header("Weapon Stats")]
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _attackInterval = 0.5f;
        [SerializeField] private float _projectileSpeed = 15f;
        [SerializeField] private float _projectileRotationSpeed = 10f;
        [SerializeField] private int _maxSimultaneousTargets = 1;
        
        [Header("Projectile")]
        [SerializeField] private Projectile _projectilePrefab;
        
        [Header("Pooling")]
        [SerializeField] private int _projectilePoolSize = 10;
        
        private ObjectPool<Projectile> _projectilePool;
        private List<Target> _activeTargets;
        private List<Target> _targetsQueue;
        
        private float _attackTimer;

        private void Awake()
        {
            _projectilePool = new ObjectPool<Projectile>(_projectilePoolSize, _projectilePrefab, transform);
        }
        
        private void Start()
        {
            _activeTargets = new List<Target>(_maxSimultaneousTargets);
            _targetsQueue = new List<Target>();
        }
        
        private void Update()
        {
            if (_activeTargets.Count == 0) return;

            _attackTimer -= Time.deltaTime;
            if (_attackTimer <= 0f)
            {
                LaunchProjectiles();
                _attackTimer = _attackInterval;
            }
        }

        private void OnDisable()
        {
            ClearAllActiveTargets();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damageable)) return;
            if (!other.TryGetComponent(out IMortal mortal)) return;
            if (!mortal.IsAlive) return;

            var target = new Target(mortal, damageable);

            if (_activeTargets.Count < _maxSimultaneousTargets)
            {
                AddActiveTarget(target);
            }
            else
            {
                _targetsQueue.Add(target);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
           if (!other.TryGetComponent(out IDamageable damageable)) return;

           if (RemoveActiveTarget(damageable.GetAttackPoint))
           {
               TrySetNextTarget();
           }
           else
           {
               RemoveFromTargetQueue(damageable.GetAttackPoint);
           }
        }
        
        private void AddActiveTarget(Target target)
        {
            if (_activeTargets.Count >= _maxSimultaneousTargets) return;
            
            _activeTargets.Add(target);
            _attackTimer = 0f;
            
            target.OnDeathHandler = () => OnTargetDeath(target);
            target.Mortal.OnDeath += target.OnDeathHandler;
        }
        
        private bool RemoveActiveTarget(Transform attackPoint)
        {
            for (int i = _activeTargets.Count - 1; i >= 0; i--)
            {
                if (_activeTargets[i].AttackPoint == attackPoint)
                {
                    _activeTargets[i].Mortal.OnDeath -= _activeTargets[i].OnDeathHandler;
                    _activeTargets.RemoveAt(i);
                    return true;
                }
            }
            
            return false;
        }
        
        private void ClearAllActiveTargets()
        {
            foreach (var target in _activeTargets)
            {
                target.Mortal.OnDeath -= target.OnDeathHandler;
            }
            
            _activeTargets.Clear();
        }
        
        private void OnTargetDeath(Target deadTarget)
        {
            _activeTargets.Remove(deadTarget);
            TrySetNextTarget();
        }
        
        private void TrySetNextTarget()
        {
            while (_targetsQueue.Count > 0 && _activeTargets.Count < _maxSimultaneousTargets)
            {
                var target = _targetsQueue[0];
                _targetsQueue.RemoveAt(0);
                
                if (target.IsValid)
                {
                    AddActiveTarget(target);
                }
            }
        }
        
        private void RemoveFromTargetQueue(Transform attackPoint)
        {
           _targetsQueue.RemoveAll(target => target.AttackPoint == attackPoint);
        }

        private void LaunchProjectiles()
        {
            for (int i = _activeTargets.Count - 1; i >= 0; i--)
            {
                var target = _activeTargets[i];
                
                if (!target.IsValid)
                {
                    _activeTargets.RemoveAt(i);
                    continue;
                }

                Transform attackPoint = target.AttackPoint;
                Vector3 direction = (attackPoint.position - _weaponPivot.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction);
                
                Projectile projectile = _projectilePool.Get();
                projectile.transform.position = _weaponPivot.position;
                projectile.transform.rotation = rotation;
                projectile.OnProjectileFinished += HandleProjectileFinished;
                projectile.Initialize(target, _damage, _projectileSpeed, _projectileRotationSpeed);

            }
            
            TrySetNextTarget();
        }
        
        private void HandleProjectileFinished(Projectile projectile)
        {
            projectile.OnProjectileFinished -= HandleProjectileFinished;
            projectile.ResetState();
            _projectilePool.Return(projectile);
        }
    }
}