using UnityEngine;

namespace DefaultNamespace.Animations.Actions
{
    public class MiningAnimationAction : IAnimationAction
    {
        private static readonly int _isMining = Animator.StringToHash("IsMining");
        
        public void Play(Animator animator)
        {
            animator.SetBool(_isMining, true);
        }

        public void Stop(Animator animator)
        {
            animator.SetBool(_isMining, false);
        }
    }
}