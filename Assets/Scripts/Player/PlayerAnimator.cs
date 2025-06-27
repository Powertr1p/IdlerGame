using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    private Animator _animator;
    private readonly int _isRunning = Animator.StringToHash("IsRunning");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(_isRunning, _playerMovement.IsRunning);
    }
}