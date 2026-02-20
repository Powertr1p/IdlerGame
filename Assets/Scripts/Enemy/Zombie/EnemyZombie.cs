using ShareComponents;
using UnityEngine;

namespace Enemy.Zombie
{
    public class EnemyZombie : EnemyBase
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int RunningAnimation = Animator.StringToHash("isRunning");
        private static readonly int AttackAnimation = Animator.StringToHash("attack");
        private static readonly int DeathAnimation = Animator.StringToHash("death");
        
        protected override void DealDamage()
        {
            if (GetTarget.TryGetComponent(out IDamageable damageable))
            {
                //todo: damage to scriptable setting
                damageable.TakeDamage(1);
            }
        }

        protected override void PlayAttackAnimation()
        {
            _animator.SetBool(RunningAnimation, IsRunning);
            _animator.SetTrigger(AttackAnimation);
        }

        protected override void PlayDeathAnimation()
        {
        }

        protected override void PlayMovementAnimation()
        {
            _animator.SetBool(RunningAnimation, IsRunning);
        }

        protected override void PlayIdleAnimation()
        {
            _animator.SetBool(RunningAnimation, IsRunning);
        }
    }
}
