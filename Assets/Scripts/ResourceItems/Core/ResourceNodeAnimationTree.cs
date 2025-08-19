using System;
using DG.Tweening;
using UnityEngine;

namespace GameItems.Core
{
    public class ResourceNodeAnimationTree : ResourceNodeAnimationBase
    {
        //енам временный, для выбора более подходящей анимации, под каждый надо настраивать power и duration
        private enum AnimationType
        {
            RotateAndBack,
            Punch,
            EndlessRotate
        }

        [SerializeField] private AnimationType _animationType;

        public override void AnimateOnHit()
        {
            KillSequence();

            switch (_animationType)
            {
                case AnimationType.RotateAndBack:
                    DoRotateAndBackTest();
                    break;
                case AnimationType.Punch:
                    DoPunchTest();
                    break;
                case AnimationType.EndlessRotate:
                    DoEndlessRotateTest();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DoRotateAndBackTest()
        {
            Quaternion originalRotation = transform.rotation;

            Sequence = DOTween.Sequence()
                .Append(transform.DORotate(new Vector3(0, 30, 0), AnimationDuration, RotateMode.LocalAxisAdd))
                .Append(transform.DORotate(originalRotation.eulerAngles, AnimationDuration))
                .SetEase(Ease.OutBack)
                .OnStart(PlayParticles);
        }

        private void DoPunchTest()
        {
            Sequence = DOTween.Sequence()
                .Append(transform.DOPunchPosition(Vector3.up * AnimationPower, AnimationDuration))
                .SetEase(Ease.OutBounce)
                .OnStart(PlayParticles);
        }

        private void DoEndlessRotateTest()
        {
            Sequence = DOTween.Sequence()
                .Append(transform.DORotate(new Vector3(0, 30, 0), AnimationDuration, RotateMode.LocalAxisAdd))
                .SetEase(Ease.OutBack)
                .OnStart(PlayParticles);
        }
    }
}