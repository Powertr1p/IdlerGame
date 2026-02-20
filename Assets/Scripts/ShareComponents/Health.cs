using System;
using UnityEngine;

namespace ShareComponents
{
   public class Health : MonoBehaviour
   {
      [SerializeField] private int _maxHealth;
      
      public event Action OnDie;
      public event Action OnTakeDamage;
      
      public  int CurrentHealth { get; private set; }
      
      private void Start()
      {
         CurrentHealth = _maxHealth;
      }

      public void ApplyDamage(int damage)
      {
         CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
         OnTakeDamage?.Invoke();

         if (CurrentHealth == 0)
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
