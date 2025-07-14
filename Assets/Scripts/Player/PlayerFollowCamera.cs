using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    [Header("Player")] 
    [SerializeField] private Transform _target;

    [Header("Smoothing")] 
    [SerializeField, Range(0f, 25f)] private float _smoothSpeed;
    
    [Header("Camera Offset")] 
    [SerializeField, Range(1f, 25f)] private float _offsetY;
    [SerializeField, Range(-25f, 25f)] private float _offsetZ;

    private float _offsetX;
    
    private Vector3 _offset => new (_offsetX, _offsetY, _offsetZ);
    
    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 desiredPosition = _target.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
