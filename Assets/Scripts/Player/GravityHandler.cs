using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class GravityHandler : MonoBehaviour
{
    [SerializeField] private float _gravityForce;
    [SerializeField] private float _groundedVerticalVelocity = -0.5f; 
    
    private CharacterController _characterController;
    private Vector3 _velocityVector;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        _velocityVector.y = !_characterController.isGrounded
            ? _velocityVector.y - _gravityForce * Time.deltaTime
            : _groundedVerticalVelocity;

        _characterController.Move(_velocityVector * Time.deltaTime);
    }
}

