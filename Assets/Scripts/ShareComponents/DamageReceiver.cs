using UnityEngine;

namespace ShareComponents
{
    [RequireComponent(typeof(Health))]
    public class DamageReceiver : MonoBehaviour, IDamageable
    {
        [SerializeField] private Transform _attackPoint;
        
        public Transform GetAttackPoint => _attackPoint;
        
        private Health _health;
        
        private void Awake()
        {
            _health = GetComponent<Health>();
        }
        
        public void TakeDamage(int damage)
        {
            _health.ApplyDamage(damage);
        }
    }
}