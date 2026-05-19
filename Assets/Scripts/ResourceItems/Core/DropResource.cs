using DG.Tweening;
using Inventory.Core;
using Inventory.ResourceItems;
using UnityEngine;

namespace ResourceItems.Core
{
    public class DropResource : MonoBehaviour, IAttractable
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private ParticleSystem _vfx;

        [Header("Spawn Animation")]
        [SerializeField] private float _jumpPower = 2f;
        [SerializeField] private float _jumpDuration = 1f;
        [SerializeField] private int _numJumps = 1;

        public ResourceType Type => _resourceType;
        public ItemQuality Quality => _quality;

        private Transform _attractor;
        private Sequence _attractionSequence;
        private Transform _cachedTransform;
        private Sequence _jumpSequence;
        private ResourceType _resourceType;
        private ItemQuality _quality;
        private Color _qualityTint = Color.white;
        
        private bool _isAttracting;
        private bool _moveActive;
        private float _attractionSpeed;
        
        private Vector3 _startPosition;
        private Vector3 _targetPosition;

        private void Awake()
        {
            _collider.enabled = false;
            _cachedTransform = transform;
        }
        
        private void Update()
        {
            if (!_isAttracting) return;

            var position = _attractor.position;
            var target = position;
            
            _cachedTransform.position = Vector3.MoveTowards(_cachedTransform.position, position, 20 * Time.deltaTime);

            if (Vector3.Distance(_cachedTransform.position, target) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
        
        public void Initialize(Vector3 startPosition, Vector3 targetPosition, ResourceType resourceType, ItemQuality quality = ItemQuality.Common, Color tint = default)
        {
            _resourceType = resourceType;
            _quality = quality;
            _qualityTint = tint == default ? Color.white : tint;
            _startPosition = startPosition;
            _targetPosition = targetPosition;

            ApplyTint();

            StartFlying();
        }

        private void ApplyTint()
        {
            if (ReferenceEquals(_vfx, null)) return;

            var systems = _vfx.GetComponentsInChildren<ParticleSystem>(true);
            foreach (var ps in systems)
            {
                var main = ps.main;
                main.startColor = _qualityTint;
            }
        }
        
        public void Attract(Transform attractor)
        {
            _attractor = attractor;
            StartAttraction();
        }

        private void StartFlying()
        {
            _jumpSequence = ConstructJumpSequence();
            _jumpSequence.OnComplete(OnJumpComplete);
        }
        
        private void StartAttraction()
        {
            _isAttracting = true;
            _jumpSequence?.Kill();
        }
        
        private void OnJumpComplete()
        {
            _vfx.transform.rotation = Quaternion.identity;
            _vfx.Play();
            
            _collider.enabled = true;
        }

        private Sequence ConstructJumpSequence()
        {
            Sequence jumpSequence = DOTween.Sequence();
            
            jumpSequence
                .Append(_cachedTransform
                    .DOJump(_targetPosition, _jumpPower, _numJumps, _jumpDuration)
                    .SetEase(Ease.OutQuint))
                .Join(_cachedTransform
                    .DORotate(new Vector3(Random.Range(180f, 360f), Random.Range(180f, 360f), 0), _jumpDuration, RotateMode.FastBeyond360)
                    .SetEase(Ease.OutQuad));
            
            return jumpSequence;
        }

        private void OnDestroy()
        {
            _jumpSequence?.Kill();
            _attractionSequence?.Kill();
        }
    }
}