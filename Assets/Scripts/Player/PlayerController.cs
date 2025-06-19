using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick _fixedJoystick;
    [SerializeField] private float _moveSpeed;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        _rigidbody.linearVelocity = new Vector3(_fixedJoystick.Horizontal * _moveSpeed, _rigidbody.linearVelocity.y,
            _fixedJoystick.Vertical * _moveSpeed);

        if (_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.linearVelocity);
        }
    }
}