using System;
using GameItems;
using Inventory;
using Inventory.RaidInventory;
using UnityEngine;
using Zenject;

public class PlayerGathering : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private Transform _attractor;
        
    [SerializeField] private RaidInventory _raidInventory;
        
    [Inject] private IPlayerLoadout _loadout;
        
    public event Action<IGatherable> CollidedWithResource;
        
    private IGatherable _resourceNode;
        
    private void OnEnable()
    {
        _playerAnimator.OnAnimationHit += HandleHit;
    }

    private void OnDisable()
    {
        _playerAnimator.OnAnimationHit -= HandleHit;
    }
        
    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent<IGatherable>(out var resourceNode)) return;
        if (!resourceNode.CanGather(_loadout.GetToolType())) return;
            
        if (_playerMovement.IsRunning)
        {
            if (IsBound(resourceNode))
            {
                UnbindResourceNode();
            }
                
            return;
        }
            
        TryBindResourceNode(resourceNode);
        _playerAnimator.PlayGatheringAnimationByType(resourceNode.Type);
        CollidedWithResource?.Invoke(resourceNode);
    }
        
    private void HandleHit()
    {
        TryGatherResource();
    }

    private void TryGatherResource()
    {
        var node = _resourceNode;
        if (node == null) return;

        var resourceType = node.Type;

        if (node.TryGather(_loadout.GetToolType(), _attractor))
        {
            _raidInventory.Add(resourceType);
        }
    }

    private void TryBindResourceNode(IGatherable resourceNode)
    {
        if (!IsBound(resourceNode))
        {
            _resourceNode = resourceNode;
            _resourceNode.Depleted += UnbindResourceNode;
        }
    }

    private bool IsBound(IGatherable resourceNode)
    {
        return ReferenceEquals(_resourceNode, resourceNode);
    }

    private void UnbindResourceNode()
    {
        _playerAnimator.StopCurrentGatheringAnimation();
        _resourceNode.StopGather();
            
        _resourceNode.Depleted -= UnbindResourceNode;
        _resourceNode = null;
    }
}