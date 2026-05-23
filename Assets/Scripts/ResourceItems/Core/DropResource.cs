using DG.Tweening;
using Inventory.Core;
using Inventory.ResourceItems;
using UnityEngine;

namespace ResourceItems.Core
{
    public class DropResource : MonoBehaviour, IAttractable
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _flightVfxPrefab;
        [SerializeField] private GameObject _landingVfxPrefab;

        [Header("Spawn Animation")]
        [SerializeField] private float _jumpPower = 1f;
        [SerializeField] private float _fallDuration = 0.3f;
        [SerializeField] private float _popDuration = 0.15f;
        [SerializeField, Range(0f, 1f)] private float _popStartScale = 0.4f;

        public ResourceType Type => _resourceType;
        public ItemQuality Quality => _quality;

        private Transform _attractor;
        private Sequence _attractionSequence;
        private Transform _cachedTransform;
        private Sequence _jumpSequence;
        private ResourceType _resourceType;
        private ItemQuality _quality;
        private Color _qualityTint = Color.white;
        private ParticleSystem _flightVfx;
        private ParticleSystem _landingVfx;

        private bool _isAttracting;
        private bool _moveActive;
        private float _attractionSpeed;

        private Vector3 _startPosition;
        private Vector3 _targetPosition;

        private void Awake()
        {
            _collider.enabled = false;
            _cachedTransform = transform;

            _flightVfx = SpawnVfx(_flightVfxPrefab);
            _landingVfx = SpawnVfx(_landingVfxPrefab);
        }

        private ParticleSystem SpawnVfx(GameObject prefab)
        {
            if (ReferenceEquals(prefab, null)) return null;
            var instance = Instantiate(prefab, transform);
            return instance.GetComponentInChildren<ParticleSystem>(true);
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
            _flightVfx?.Simulate(0.15f, true, true, true);
            _flightVfx?.Play();

            StartFlying();
        }

        private void ApplyTint()
        {
            ApplyTintTo(_flightVfx);
            ApplyTintTo(_landingVfx);
        }

        private void ApplyTintTo(ParticleSystem root)
        {
            if (ReferenceEquals(root, null)) return;
            var systems = root.GetComponentsInChildren<ParticleSystem>(true);
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
            PlaySpawnPop();
            _jumpSequence = ConstructJumpSequence();
            _jumpSequence.OnComplete(OnJumpComplete);
        }

        private void PlaySpawnPop()
        {
            _cachedTransform.localScale = Vector3.one * _popStartScale;
            _cachedTransform.DOScale(Vector3.one, _popDuration).SetEase(Ease.OutBack);
        }
        
        private void StartAttraction()
        {
            _isAttracting = true;
            _jumpSequence?.Kill();
        }
        
        private void OnJumpComplete()
        {
            _collider.enabled = true;
            _landingVfx?.Play();
        }

        private Sequence ConstructJumpSequence()
        {
            Sequence jumpSequence = DOTween.Sequence();

            jumpSequence.Append(_cachedTransform
                .DOJump(_targetPosition, _jumpPower, 1, _fallDuration)
                .SetEase(Ease.InQuad));

            return jumpSequence;
        }

        private void OnDestroy()
        {
            _jumpSequence?.Kill();
            _attractionSequence?.Kill();
        }
    }
}