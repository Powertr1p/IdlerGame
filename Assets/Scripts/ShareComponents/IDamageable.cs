using UnityEngine;

namespace ShareComponents
{
    public interface IDamageable
    {
        void TakeDamage(int damage);
        Transform GetAttackPoint { get; }
    }
}