using System;
using Enemy;
using ShareComponents;
using UnityEngine;

namespace PlayerWeapon
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _projectile;
        [SerializeField] private ParticleSystem _hitEffectPrefab;

        [Header("Slow Effect (0 = no slow)")]
        [SerializeField, Range(0f, 1f)] private float _slowMultiplier;
        [SerializeField] private float _slowDuration;

        public event Action<Projectile> OnProjectileFinished;
        
        private Target _target;
        private int _damage;
        private float _speed;
        private float _rotationSpeed;
        private bool _hasHit;
        private Transform _targetTransform;

        private void Update()
        {
            if (_hasHit) return;

            if (ReferenceEquals(_target, null))
            {
                OnProjectileFinished?.Invoke(this);
                return;
            }

            if (!_target.IsValid)
            {
                OnProjectileFinished?.Invoke(this);
                return;
            }

            TrackAndMoveToTarget();
            CheckDistance();
        }
        
        public void Initialize(Target target, int damage, float speed, float rotationSpeed)
        {
            _target = target;
            _damage = damage;
            _speed = speed;
            _rotationSpeed = rotationSpeed;
            
            if (_projectile != null)
            {
                _projectile.Play();
            }
        }
        
        public void ResetState()
        {
            _hasHit = false;
            _target = null;
            _damage = 0;
            _speed = 0;
            _rotationSpeed = 0;
        }
        
        private void TrackAndMoveToTarget()
        {
            Vector3 targetPosition = _target.AttackPoint.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
            transform.LookAt(targetPosition);
        }
        
        private void CheckDistance()
        {
            float distance = Vector3.Distance(transform.position, _target.AttackPoint.position);
            
            if (distance < 0.5f)
            {
                HitTarget();
            }
        }
        
        private void HitTarget()
        {
            if (_hasHit) return;

            _hasHit = true;
            _target.Damageable.TakeDamage(_damage);
            TryApplySlow();

            if (!ReferenceEquals(_projectile, null))
            {
                _projectile.Stop();
            }

            if (!ReferenceEquals(_hitEffectPrefab, null))
            {
                Instantiate(_hitEffectPrefab, transform.position, Quaternion.identity);
            }

            OnProjectileFinished?.Invoke(this);
        }

        private void TryApplySlow()
        {
            if (_slowMultiplier <= 0f || _slowDuration <= 0f) return;

            ISlowable slowable = _target.AttackPoint.GetComponentInParent<ISlowable>();
            if (ReferenceEquals(slowable, null)) return;

            slowable.ApplySlow(_slowMultiplier, _slowDuration);
        }
    }
}