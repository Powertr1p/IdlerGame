using System.Collections.Generic;
using DefaultNamespace.Animations.Actions;
using Inventory.Core;
using Inventory.ResourceItems;
using UnityEngine;

namespace DefaultNamespace.Animations
{
    public class GatheringAnimationMapper
    {
        private readonly Animator _animator;
        private readonly Dictionary<ResourceType, IAnimationAction> _animationMap;
        
        private IAnimationAction _currentAnimation;

        public GatheringAnimationMapper(Animator animator)
        {
            _animator = animator;
            
            _animationMap = new Dictionary<ResourceType, IAnimationAction>
            {
                { ResourceType.Stone, new MiningAnimationAction() },
                { ResourceType.Wood, new ChoppingAnimationAction() },
            };
        }

        public void PlayAnimation(ResourceType resourceType)
        {
            if (!_animationMap.TryGetValue(resourceType, out IAnimationAction animation)) return;
            if (_currentAnimation == animation) return;
            
            StopCurrentAnimation();
            
            _currentAnimation = animation;
            animation.Play(_animator);
        }

        public void StopCurrentAnimation()
        {
            if (_currentAnimation != null)
            {
                _currentAnimation.Stop(_animator);
                _currentAnimation = null;
            }
        }
    }
}