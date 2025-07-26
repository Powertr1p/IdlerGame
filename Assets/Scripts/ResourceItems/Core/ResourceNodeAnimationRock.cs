using DG.Tweening;

namespace GameItems.Core
{
    public class ResourceNodeAnimationRock : ResourceNodeAnimationBase
    {
        public override void AnimateOnHit()
        {
            KillSequence();

            CurrentPulseSequence = DOTween.Sequence();

            CurrentPulseSequence.Append(
                transform.DOScale(OriginalScale * PulsePower, PulseDuration / 2)
                    .SetEase(Ease.OutQuad)
            );

            CurrentPulseSequence.Append(
                transform.DOScale(OriginalScale, PulseDuration / 2)
                    .SetEase(Ease.OutElastic, 1, 0.5f)
            );
        }
    }
}