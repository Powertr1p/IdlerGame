using DG.Tweening;
using UnityEngine;

namespace GameItems
{
    [RequireComponent(typeof(ResourceNode))]
    public abstract class ResourceNodeAnimationBase : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] protected float _pulseScale = 0.9f;
        [SerializeField] protected float _pulseDuration = 0.2f;

        protected Sequence _currentPulseSequence;
        protected Vector3 _originalScale;

        protected void Awake() 
        {
            _originalScale = transform.localScale;
        }
        
        protected void OnDestroy()
        {
            KillSequence();
        }

        public abstract void AnimateOnHit();

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