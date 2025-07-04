using DefaultNamespace;
using GameItems;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private PlayerGathering _playerGathering;
    
    public bool IsRunning => _joystick.Horizontal != 0 || _joystick.Vertical != 0;

    private CharacterController _characterController;
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _playerGathering.CollidedWithResource += LookAtResource;
    }

    private void OnDisable()
    {
        _playerGathering.CollidedWithResource -= LookAtResource;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void LookAtResource(IGatherable resource)
    { 
        if (!IsRunning)
        {
            transform.LookAt(resource.Transform); 
        }
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
        _characterController.Move(moveDirection * (_moveSpeed * Time.deltaTime));

        if (moveDirection == Vector3.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
}