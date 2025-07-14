using DG.Tweening;
using UnityEngine;

namespace GameItems
{
    [RequireComponent(typeof(ResourceNode))]
    public class ResourceNodeAnimationBase : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] protected float _pulseScale = 0.9f;
        [SerializeField] protected float _pulseDuration = 0.2f;
        
        private Sequence _currentPulseSequence;
        private Vector3 _originalScale;

        protected virtual void Awake() 
        {
            _originalScale = transform.localScale;
        }
        
        protected virtual void OnDestroy()
        {
            KillSequence();
        }
        
        public virtual void AnimateResourcePulse() 
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

        public virtual void KillSequence()
        {
            if (_currentPulseSequence != null && _currentPulseSequence.IsActive())
            {
                _currentPulseSequence.Kill();
                _currentPulseSequence = null;
            }
        }
    }
}