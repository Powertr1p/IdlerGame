using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class GravityHandler : MonoBehaviour
{
    [SerializeField] private float _gravityForce;
    
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
        if (!_characterController.isGrounded)
        {
            _velocityVector.y -= _gravityForce * Time.deltaTime;
        }
        else
        {
            _velocityVector.y = -0.5f;
        }

        _characterController.Move(_velocityVector * Time.deltaTime);
    }
}

