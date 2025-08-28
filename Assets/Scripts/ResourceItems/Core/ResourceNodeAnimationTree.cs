using DG.Tweening;
using UnityEngine;

namespace GameItems.Core
{
    public class ResourceNodeAnimationTree : ResourceNodeAnimationBase
    {
        public override void AnimateOnHit()
        {
            KillSequence();
            DoRotate();
        }

        private void DoRotate()
        {
            Quaternion originalRotation = transform.rotation;

            Sequence = DOTween.Sequence()
                .Append(transform.DORotate(new Vector3(0, 30, 0), AnimationDuration, RotateMode.LocalAxisAdd))
                .Append(transform.DORotate(originalRotation.eulerAngles, AnimationDuration))
                .SetEase(Ease.OutBack)
                .OnStart(PlayParticles);
        }
    }
}