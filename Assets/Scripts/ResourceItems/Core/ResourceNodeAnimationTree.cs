using DG.Tweening;
using UnityEngine;

namespace GameItems.Core
{
    public class ResourceNodeAnimationTree : ResourceNodeAnimationBase
    {
        public override void AnimateOnHit()
        {
            KillSequence();

            CurrentPulseSequence = DOTween.Sequence()
                .Append(transform.DOPunchPosition(Vector3.forward * PulsePower, PulseDuration))
                .OnStart(PlayParticles);
        }
    }
}