using DG.Tweening;
using UnityEngine;

namespace GameItems
{
    [RequireComponent(typeof(ResourceNode))]
    public abstract class ResourceNodeAnimationBase : MonoBehaviour
    {
        [Header("Pulse Animation")] 
        [SerializeField] protected float PulsePower = 0.9f;
        [SerializeField] protected float PulseDuration = 0.2f;

        [Header("Particles")] 
        [SerializeField] protected ParticleSystem Particles;
        [SerializeField] protected Transform ParticlesPivot;

        protected Sequence CurrentPulseSequence;
        protected Vector3 OriginalScale;

        private void Awake()
        {
            OriginalScale = transform.localScale;
        }

        private void OnDestroy()
        {
            KillSequence();
        }
        
        public abstract void AnimateOnHit();

        public void KillSequence()
        {
            if (CurrentPulseSequence != null && CurrentPulseSequence.IsActive())
            {
                CurrentPulseSequence.Kill();
                CurrentPulseSequence = null;
            }
        }

        protected void PlayParticles()
        {
            if (!ReferenceEquals(Particles, null))
            {
                Transform particlesPivotTransform = ParticlesPivot.transform;
                
                ParticleSystem particles = Instantiate(
                    Particles,
                    particlesPivotTransform.position,
                    Quaternion.identity,
                    particlesPivotTransform);
                
                particles.Play();
            }
        }
    }
}