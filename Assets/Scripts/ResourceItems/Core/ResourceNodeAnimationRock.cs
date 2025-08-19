using DG.Tweening;
using UnityEngine;

namespace GameItems.Core
{
    public class ResourceNodeAnimationRock : ResourceNodeAnimationBase
    {
        private Vector3 _originalScale;
        
        private void Awake()
        {
            _originalScale = transform.localScale;
        }
        
        public override void AnimateOnHit()
        {
            KillSequence();

            Sequence = DOTween.Sequence();

            Sequence.Append(
                transform.DOScale(_originalScale * AnimationPower, AnimationDuration / 2)
                    .SetEase(Ease.OutQuad)
            );

            Sequence.Append(
                transform.DOScale(_originalScale, AnimationDuration / 2)
                    .SetEase(Ease.OutElastic, 1, 0.5f)
            );
        }
    }
}