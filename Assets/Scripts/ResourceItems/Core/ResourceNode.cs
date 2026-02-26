using System;
using Inventory.Core;
using Inventory.EquipmentItems;
using Inventory.ResourceItems;
using JetBrains.Annotations;
using ResourceItems.Core;
using Scriptable;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameItems
{
    public class ResourceNode : MonoBehaviour, IGatherable
    { 
        [SerializeField] private ResourceData _resourceData;
        [SerializeField] private float _spreadRadius = 3f;
        [SerializeField] private ResourceNodeAnimationBase _animation;
        
        [CanBeNull] private Transform _droppedItemsAttractor = null;
        
        public event Action Depleted;
        
        public Transform Transform { get; private set; }
        public ResourceType Type { get; private set; }
        
        private int _currentHits;
        private int _spawnedCount;
        private int _hitsToGather;
        private int _hitsToDeplete;
        private bool _isInitialized;
        
        private void Awake()
        {
            Type = _resourceData.ResourceType;
            
            Transform = transform;
        }

        public bool TryGather(ToolType toolType, Transform attractor)
        {
            _droppedItemsAttractor = attractor;
            _animation.AnimateOnHit();

            _currentHits++;

            if (_currentHits % GetNeededHitsToGather(toolType) != 0) return false;
            
            SpawnDropItem();
            
            if (!IsRemain(toolType))
            {
                Depleted?.Invoke();
                Destroy(gameObject);
            }
            
            return true;
        }

        public bool IsRightTool(ToolType toolType)
        {
            return toolType == _resourceData.ToolType && IsRemain(toolType);
        }

        private bool IsRemain(ToolType tool)
        {
            return _currentHits < GetNeededHitsToDeplete(tool);
        }

        private void SpawnDropItem()
        {
            Vector3 startPosition = transform.position;
            Vector2 randomDirection = Random.insideUnitCircle.normalized;

            float x = startPosition.x + randomDirection.x * _spreadRadius;
            float z = startPosition.z + randomDirection.y * _spreadRadius;

            Vector3 targetPosition = new Vector3(x, 0f, z);

            DropResource dropItem = Instantiate(_resourceData.ResourcePrefab, startPosition, Quaternion.identity);
            dropItem.Initialize(_droppedItemsAttractor, startPosition, targetPosition);

            _spawnedCount++;
        }
        
        public void StopGather()
        {
            _droppedItemsAttractor = null;
            _animation.KillSequence();
        }

        private int GetNeededHitsToGather(ToolType toolType)
        {
            return toolType == _resourceData.ToolType ? _resourceData.HitsToGather : _resourceData.HitsToGather * 2;
        }
        
        private int GetNeededHitsToDeplete(ToolType toolType)
        {
            return toolType == _resourceData.ToolType ? _resourceData.HitsToDeplete : _resourceData.HitsToDeplete * 2;
        }
    }
}