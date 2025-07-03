using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    public event Action CollidedWithResource;
    
    public bool IsRunning => _joystick.Horizontal != 0 || _joystick.Vertical != 0;

    private CharacterController _characterController;
    private Transform _lookTarget;
    

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsRunning)
        {
            transform.LookAt(other.transform);
        }
        
        CollidedWithResource?.Invoke();
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