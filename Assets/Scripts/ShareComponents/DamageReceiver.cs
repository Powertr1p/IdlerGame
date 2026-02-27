using System;
using UnityEngine;

namespace ShareComponents
{
    [RequireComponent(typeof(Health))]
    public class DamageReceiver : MonoBehaviour, IDamageable, IMortal
    {
        [SerializeField] private Transform _attackPoint;
        
        public event Action OnDeath;
        
        public Transform GetAttackPoint => _attackPoint;
        public bool IsAlive => _health.CurrentHealth > 0;
        
        private Health _health;
        
        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.OnDie += HandleDeath;
        }

        private void OnDisable()
        {
            _health.OnDie -= HandleDeath;
        }

        public void TakeDamage(int damage)
        {
            _health.ApplyDamage(damage);
        }

        private void HandleDeath()
        {
            OnDeath?.Invoke();
        }
    }
}