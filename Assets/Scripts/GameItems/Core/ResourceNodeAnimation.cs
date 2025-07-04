using DG.Tweening;
using UnityEngine;

namespace GameItems
{
    public class ResourceNodeAnimation : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] private float _pulseScale = 0.9f;
        [SerializeField] private float _pulseDuration = 0.2f;
        
        private Sequence _currentPulseSequence;
        private Vector3 _originalScale;

        private void Awake()
        {
            _originalScale = transform.localScale;
        }
        
        private void OnDestroy()
        {
            if (_currentPulseSequence != null && _currentPulseSequence.IsActive())
            {
                _currentPulseSequence.Kill();
                _currentPulseSequence = null;
            }
        }
        
        public void AnimateResourcePulse()
        {
            if (_currentPulseSequence != null && _currentPulseSequence.IsActive())
            {
                _currentPulseSequence.Kill();
            }
            
            _currentPulseSequence = DOTween.Sequence();
            
            _currentPulseSequence.Append(
                transform.DOScale(_originalScale * _pulseScale, _pulseDuration / 2)
                    .SetEase(Ease.OutQuad)
            );
            
            _currentPulseSequence.Append(
                transform.DOScale(_originalScale, _pulseDuration / 2)
                    .SetEase(Ease.OutElastic, 1, 0.5f)
            );
        }

        public void KillSequence()
        {
            if (_currentPulseSequence != null && _currentPulseSequence.IsActive())
            {
                _currentPulseSequence.Kill();
                _currentPulseSequence = null;
            }
        }
    }
}