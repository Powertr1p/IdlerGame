using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameItems
{
    [RequireComponent(typeof(ResourceNode))]
    public abstract class ResourceNodeAnimationBase : MonoBehaviour
    {
        [Header("Pulse Animation")] 
        [SerializeField] protected float _pulsePower = 0.9f;
        [SerializeField] protected float _pulseDuration = 0.2f;

        protected Sequence _currentPulseSequence;
        protected Vector3 _originalScale;

        private void Awake()
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