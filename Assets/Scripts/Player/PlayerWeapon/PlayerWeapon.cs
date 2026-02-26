using JetBrains.Annotations;
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
        
        [Header("Projectile")]
        [SerializeField] private Projectile _projectilePrefab;

        [CanBeNull] private Transform _target;
        
        private float _attackTimer;
        
        private void Update()
        {
            if (ReferenceEquals(_target, null)) return;

            _attackTimer -= Time.deltaTime;
            if (_attackTimer <= 0f)
            {
                LaunchProjectile();
                _attackTimer = _attackInterval;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Player weapon hit collider {other.name}");
            
            if (other.TryGetComponent(out IDamageable damageable))
            {
                _target = damageable.GetAttackPoint;
                _attackTimer = 0f;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!ReferenceEquals(_target, null) && other.transform == _target)
            {
                _target = null;
            }
        }
        

        private void LaunchProjectile()
        {
            if (ReferenceEquals(_target, null)) return;

            Vector3 direction = (_target.position - _weaponPivot.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            
            Projectile projectile = Instantiate(_projectilePrefab, _weaponPivot.position, rotation);
            projectile.Initialize(_target, _damage, _projectileSpeed, _projectileRotationSpeed);
        }
    }
}