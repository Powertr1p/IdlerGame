using DG.Tweening;

namespace GameItems.Core
{
    public class ResourceNodeAnimationRock : ResourceNodeAnimationBase
    {
        public override void AnimateOnHit()
        {
            if (_currentPulseSequence != null && _currentPulseSequence.IsActive())
            {
                _currentPulseSequence.Kill();
            }

            _currentPulseSequence = DOTween.Sequence();

            _currentPulseSequence.Append(
                transform.DOScale(_originalScale * _pulsePower, _pulseDuration / 2)
                    .SetEase(Ease.OutQuad)
            );

            _currentPulseSequence.Append(
                transform.DOScale(_originalScale, _pulseDuration / 2)
                    .SetEase(Ease.OutElastic, 1, 0.5f)
            );
        }
    }
}