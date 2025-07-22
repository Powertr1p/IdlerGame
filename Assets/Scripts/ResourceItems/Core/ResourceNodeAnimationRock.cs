using DG.Tweening;
using UnityEngine;

namespace GameItems.Core
{
    public class ResourceNodeAnimationRock : ResourceNodeAnimationBase
    {
        [SerializeField] private float _scaleReduction = 0.1f;
        [SerializeField] private float _minScale = 0.1f;
        [SerializeField] private float _yOffsetPerHit;

        private float _currentYOffset;
        private Vector3 _originalPosition;

        protected override void OnAwake()
        {
            _originalPosition = transform.position;
        }

        public override void AnimateOnHit()
        {
            KillSequence();

            Vector3 currentScale = transform.localScale;
            Vector3 targetScale = CalculateTargetScale(currentScale);
            Vector3 targetPosition = CalculateTargetPosition();

            _currentPulseSequence = DOTween.Sequence();

            _currentPulseSequence.Append(
                transform.DOScale(currentScale * _pulseScale, _pulseDuration / 2)
                    .SetEase(Ease.OutQuad)
            );

            _currentPulseSequence.Append(
                transform.DOScale(targetScale, _pulseDuration / 2)
                    .SetEase(Ease.OutElastic)
            );

            _currentPulseSequence.Join(
                    transform.DOMove(targetPosition, _pulseDuration / 2))
                .SetEase(Ease.OutElastic, 1, 0.5f
                );
        }

        private Vector3 CalculateTargetPosition()
        {
            _currentYOffset -= _yOffsetPerHit;
            Vector3 targetPosition = _originalPosition + new Vector3(0, _currentYOffset, 0);
            return targetPosition;
        }

        private Vector3 CalculateTargetScale(Vector3 currentScale)
        {
            float newScaleX = Mathf.Max(currentScale.x - _scaleReduction, _minScale);
            float newScaleY = Mathf.Max(currentScale.y - _scaleReduction, _minScale);
            float newScaleZ = Mathf.Max(currentScale.z - _scaleReduction, _minScale);

            Vector3 targetScale = new Vector3(newScaleX, newScaleY, newScaleZ);
            return targetScale;
        }
    }
}