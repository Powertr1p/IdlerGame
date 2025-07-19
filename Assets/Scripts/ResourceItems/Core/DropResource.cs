using DG.Tweening;
using UnityEngine;

namespace GameItems
{
    public class DropResource : MonoBehaviour
    {
        [SerializeField] private float _attractionDuration = 0.5f;
       
        [Header("Spawn Animation")]
        [SerializeField] private float _jumpPower = 2f;
        [SerializeField] private float _jumpDuration = 1f;
        [SerializeField] private int _numJumps = 1;
        
        [Header("Attraction Animation")]
        [SerializeField] private float _bounceHeight = 0.5f;
        [SerializeField] private float _rotationSpeed = 360f;
        [SerializeField] private float _attractionStartDelay = 0.1f;
        [SerializeField] private float _attractionDelay = 0.1f;
        
        private Transform _attractor;
        private Sequence _attractionSequence;

        private bool _isAttracting;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        
        public void Initialize(Transform attractor, Vector3 startPosition, Vector3 targetPosition)
        {
            _attractor = attractor;
            _startPosition = startPosition;
            _targetPosition = targetPosition;
            
            StartFlying();
        }

        private void StartFlying()
        {
            Sequence jumpSequence = ConstructJumpSequence();
            float attractionStartTime = _jumpDuration - _attractionStartDelay;
            
            if (attractionStartTime > 0)
            {
                jumpSequence.InsertCallback(attractionStartTime, StartAttraction);
            }
            else
            {
                jumpSequence.OnComplete(StartAttraction);
            }
        }
        
        private void StartAttraction()
        {
            if (_isAttracting || ReferenceEquals(_attractor, null)) return;
            
            _isAttracting = true;
            
            ConstructAttractionSequence();
        }

        private Sequence ConstructJumpSequence()
        {
            Sequence jumpSequence = DOTween.Sequence();
            
            jumpSequence
                .Append(transform
                    .DOJump(_targetPosition, _jumpPower, _numJumps, _jumpDuration)
                    .SetEase(Ease.OutQuint))
                .Join(transform
                    .DORotate(new Vector3(Random.Range(180f, 360f), Random.Range(180f, 360f), 0), _jumpDuration, RotateMode.FastBeyond360)
                    .SetEase(Ease.OutQuad));
            
            return jumpSequence;
        }

        private void ConstructAttractionSequence()
        {
            _attractionSequence = DOTween.Sequence();
            
            _attractionSequence
                .Append(transform.DOLocalMoveY(transform.position.y + _bounceHeight, _attractionDelay)
                    .SetEase(Ease.OutQuad))
                .Append(transform.DOMove(_attractor.position, _attractionDuration)
                    .SetEase(Ease.InCubic))
                .Join(transform.DORotate(new Vector3(0, _rotationSpeed, 0), _attractionDuration, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear))
                .Join(transform.DOScale(Vector3.zero, _attractionDuration * 0.7f)
                    .SetEase(Ease.InExpo)
                    .SetDelay(_attractionDuration * 0.3f))
                .OnComplete(() => 
                { 
                    Destroy(gameObject); 
                });
        }

        private void OnDestroy()
        {
            _attractionSequence?.Kill();
        }
    }
}