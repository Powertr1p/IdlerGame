using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private const string IsRunning = "IsRunning";
    
    [SerializeField] private PlayerMovement _playerMovement;
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(IsRunning, _playerMovement.IsRunning());
    }
}
