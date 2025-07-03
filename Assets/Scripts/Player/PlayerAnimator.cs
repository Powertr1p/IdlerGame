using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    private Animator _animator;
    
    private readonly int _isRunning = Animator.StringToHash("IsRunning");
    private readonly int _isMining = Animator.StringToHash("IsMining");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerMovement.CollidedWithResource += MiningAnimation;
    }

    private void OnDisable()
    {
        _playerMovement.CollidedWithResource -= MiningAnimation;
    }

    private void Update()
    {
        _animator.SetBool(_isRunning, _playerMovement.IsRunning);
    }

    private void MiningAnimation()
    {
        _animator.SetBool(_isMining, !_playerMovement.IsRunning);
    }
}