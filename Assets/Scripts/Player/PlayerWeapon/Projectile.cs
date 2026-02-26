using ShareComponents;
using UnityEngine;

namespace PlayerWeapon
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _projectile;
        [SerializeField] private ParticleSystem _hitEffectPrefab;

        private Transform _target;
        private int _damage;
        private float _speed;
        private float _rotationSpeed;
        private bool _hasHit;
        
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
        
        public void Initialize(Transform target, int damage, float speed, float rotationSpeed)
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
            Vector3 direction = (_target.position - transform.position).normalized;
            
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            
            transform.position += transform.forward * _speed * Time.deltaTime;
        }
        
        private void CheckDistance()
        {
            float distance = Vector3.Distance(transform.position, _target.position);
            
            if (distance < 0.5f)
            {
                HitTarget();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (_hasHit) return;

            if (other.transform == _target)
            {
                HitTarget();
            }
        }
        
        private void HitTarget()
        {
            if (_hasHit) return;
    
            _hasHit = true;

            if (_target.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
            }
    
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