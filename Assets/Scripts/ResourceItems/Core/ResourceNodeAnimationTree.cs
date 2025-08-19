using DG.Tweening;
using UnityEngine;

namespace GameItems.Core
{
    public class ResourceNodeAnimationTree : ResourceNodeAnimationBase
    {
        public override void AnimateOnHit()
        {
            KillSequence();
            
            //на выбор один из тестов
            DoRotateAndBackTest();
            //DoPunchTest();
            //DoEndlessRotateTest();
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