using System.Collections.Generic;
using DefaultNamespace.Animations.Actions;
using Inventory.Core;
using UnityEngine;

namespace DefaultNamespace.Animations
{
    public class GatheringAnimationMapper
    {
        private readonly Animator _animator;
        private readonly Dictionary<ItemType, IAnimationAction> _animationMap;
        
        private IAnimationAction _currentAnimation;

        public GatheringAnimationMapper(Animator animator)
        {
            _animator = animator;
            
            _animationMap = new Dictionary<ItemType, IAnimationAction>
            {
                { ItemType.Rock, new MiningAnimationAction() },
            };
        }

        public void PlayAnimation(ItemType itemType)
        {
            if (!_animationMap.TryGetValue(itemType, out IAnimationAction animation)) return;
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