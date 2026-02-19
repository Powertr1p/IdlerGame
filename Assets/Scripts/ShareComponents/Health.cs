using System;
using UnityEngine;

namespace ShareComponents
{
   public class Health : MonoBehaviour
   {
      [SerializeField] private int _maxHealth;
      private int _currentHealth;

      public event Action OnDie;
      public event Action OnTakeDamage;

      private void Start()
      {
         _currentHealth = _maxHealth;
      }

      public void DealDamage(int damage)
      {
         _currentHealth = Mathf.Min(0, _currentHealth - damage);
         OnTakeDamage?.Invoke();

         if (_currentHealth == 0)
         {
            Die();
         }
      }
      
      private void Die()
      {
         OnDie?.Invoke();
      }
   }
}
