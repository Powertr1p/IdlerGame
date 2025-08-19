using DG.Tweening;
using UnityEngine;

namespace GameItems
{
    [RequireComponent(typeof(ResourceNode))]
    public abstract class ResourceNodeAnimationBase : MonoBehaviour
    {
        [Header("Pulse Animation")] 
        [SerializeField] protected float AnimationPower = 0.9f;
        [SerializeField] protected float AnimationDuration = 0.2f;

        [Header("Particles")] 
        [SerializeField] protected ParticleSystem Particles;
        [SerializeField] protected Transform ParticlesPivot;

        protected Sequence Sequence;

        private void OnDestroy()
        {
            KillSequence();
        }
        
        public abstract void AnimateOnHit();

        public void KillSequence()
        {
            if (Sequence != null && Sequence.IsActive())
            {
                Sequence.Kill();
                Sequence = null;
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