using UnityEngine;

namespace DefaultNamespace.Animations.Actions
{
    public class ChoppingAnimationAction : IAnimationAction
    {
        private static readonly int _isChopping = Animator.StringToHash("IsChopping");
        
        public void Play(Animator animator)
        {
            animator.SetBool(_isChopping, true);
        }

        public void Stop(Animator animator)
        {
            animator.SetBool(_isChopping, false);
        }
    }
}
