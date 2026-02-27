using ShareComponents;
using UnityEngine;

namespace PlayerWeapon
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _projectile;
        [SerializeField] private ParticleSystem _hitEffectPrefab;

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
                Destroy(gameObject);
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
            
            if (!ReferenceEquals(_projectile, null))
            {
                _projectile.Stop();
            }
    
            if (!ReferenceEquals(_hitEffectPrefab, null))
            {
                Instantiate(_hitEffectPrefab, transform.position, Quaternion.identity);
            }
    
            Destroy(gameObject, 0.1f);
        }
    }
}