using System;
using DefaultNamespace.Animations;
using Inventory.Core;
using Inventory.ResourceItems;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    public event Action OnAnimationHit;
    
    private Animator _animator;
    private GatheringAnimationMapper _mapper;
    
    private readonly int _isRunning = Animator.StringToHash("IsRunning");
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _mapper = new GatheringAnimationMapper(GetComponent<Animator>());
    }
    
    //используется ключем анимации
    public void OnHit()
    {
        OnAnimationHit?.Invoke();
    }

    private void Update()
    {
        _animator.SetBool(_isRunning, _playerMovement.IsRunning);
    }

    public void PlayGatheringAnimationByType(ResourceType type)
    {
        _mapper.PlayAnimation(type);
    }

    public void StopCurrentGatheringAnimation()
    {
       _mapper.StopCurrentAnimation();
    }
}