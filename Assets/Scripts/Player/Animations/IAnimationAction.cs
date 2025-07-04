using UnityEngine;

namespace DefaultNamespace.Animations
{
    public interface IAnimationAction
    {
        void Play(Animator animator);
        void Stop(Animator animator);
    }
}