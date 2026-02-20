using UnityEngine;

namespace ShareComponents
{
    [RequireComponent(typeof(Health))]
    public class DamageReceiver : MonoBehaviour, IDamageable
    {
        private Health _health;
        
        private void Awake()
        {
            _health = GetComponent<Health>();
        }
        
        public void TakeDamage(int damage)
        {
            _health.ApplyDamage(damage);
            Debug.Log(_health.CurrentHealth);
        }
    }
}