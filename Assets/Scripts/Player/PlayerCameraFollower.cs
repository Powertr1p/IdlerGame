using UnityEngine;

public class PlayerCameraFollower : MonoBehaviour
{
    [Header("Player")] 
    [SerializeField] private Transform _target;

    [Header("Smoothing")] 
    [SerializeField, Range(0f, 15f)] private float _smoothSpeed;
    
    [Header("Camera Offset")] 
    [SerializeField, Range(1f, 25f)] private float _offsetY;
    [SerializeField, Range(-25f, 25f)] private float _offsetZ;

    private Vector3 _offset => new(0, _offsetY, _offsetZ);
    private Transform _cachedTransform;

    private void Awake()
    {
        _cachedTransform = transform;
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void OnValidate()
    {
        transform.position = CalculateDesiredPosition();
    }

    private void FollowTarget()
    {
        Vector3 desiredPosition = CalculateDesiredPosition();
        Vector3 smoothedPosition =
            Vector3.Lerp(_cachedTransform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        _cachedTransform.position = smoothedPosition;
    }

    private Vector3 CalculateDesiredPosition()
    {
        return _target.position + _offset;
    }
}
