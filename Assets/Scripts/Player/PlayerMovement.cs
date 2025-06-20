using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private DynamicJoystick _joystick;
    [SerializeField] private float _moveSpeed;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
        _characterController.Move(moveDirection * (_moveSpeed * Time.deltaTime));

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }
}