using UnityEngine;

namespace Enemy.Zombie
{
    public class EnemyZombie : EnemyBase
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int Attack = Animator.StringToHash("attack");
        private static readonly int Death = Animator.StringToHash("death");
        
        protected override void PerformAttack()
        {
        }

        protected override void PlayAttackAnimation()
        {
        }

        protected override void PlayDeathAnimation()
        {
        }

        protected override void PlayMovementAnimation()
        {
            _animator.SetBool(IsRunning, true);
        }

        protected override void PlayIdleAnimation()
        {
            _animator.SetBool(IsRunning, false);
        }
    }
}
