using DG.Tweening;
using UnityEngine;

namespace GameItems.Core
{
    public class ResourceNodeAnimationTree : ResourceNodeAnimationBase
    {
        public override void AnimateOnHit()
        {
            KillSequence();

            Sequence = DOTween.Sequence()
                //todo: дергает ротейшн (панч\либо ручками)
                .Append(transform.DOPunchPosition(Vector3.forward * PulsePower, PulseDuration))
                .OnStart(PlayParticles);
        }
    }
}