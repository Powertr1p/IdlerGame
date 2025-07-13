using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    [Header("Player")] 
    [SerializeField] private Transform _target;

    [Header("Follow Settings")] 
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private Vector3 _offset;

    private void LateUpdate()
    {
        Vector3 desiredPosition = _target.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
