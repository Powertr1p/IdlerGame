using ShareComponents;
using UnityEngine;

namespace PlayerWeapon
{
    public class Target
    {
        public readonly IMortal Mortal;
        public readonly IDamageable Damageable;
        public System.Action OnDeathHandler { get; set; }
    
        public Target(IMortal mortal, IDamageable damageable)
        {
            Mortal = mortal;
            Damageable = damageable;
        }
    
        public Transform AttackPoint => Damageable.GetAttackPoint;
        public bool IsValid => Mortal.IsAlive;
    }
}