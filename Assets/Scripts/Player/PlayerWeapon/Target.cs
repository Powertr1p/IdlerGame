using ShareComponents;
using UnityEngine;

namespace PlayerWeapon
{
    public class Target
    {
        public readonly IMortal Mortal;
        private readonly IDamageable _damageable;
        public System.Action OnDeathHandler { get; set; }
    
        public Target(IMortal mortal, IDamageable damageable)
        {
            Mortal = mortal;
            _damageable = damageable;
        }
    
        public Transform AttackPoint => _damageable.GetAttackPoint;
        public bool IsValid => Mortal.IsAlive;
    }
}