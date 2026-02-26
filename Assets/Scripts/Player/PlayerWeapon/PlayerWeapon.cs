using JetBrains.Annotations;
using ShareComponents;
using UnityEngine;

namespace PlayerWeapon
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private Transform _weaponTransform;
        [SerializeField] private float _attackInterval = 0.5f;

        [CanBeNull] private IDamageable _target;
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Player weapon hit collider {other.name}");
            
            if (other.TryGetComponent(out IDamageable damageable))
            {
                _target = damageable;
                AttackTarget();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            _target = null;
        }
        
        private void AttackTarget()
        {
            LaunchMissile();
            
            _target?.TakeDamage(1);
        }

        private void LaunchMissile()
        {
            
        }
    }
}