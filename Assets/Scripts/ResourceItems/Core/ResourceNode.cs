using System;
using Inventory.Core;
using JetBrains.Annotations;
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
        public ItemType Type { get; private set; }
        
        private int _currentHits;
        private int _spawnedCount;
        
        private void Awake()
        {
            Type = _resourceData.ItemType;
            
            Transform = transform;
        }

        public bool TryGather(ToolType toolType, Transform attractor)
        {
            //todo: хуета
            if (!CanGather(toolType)) return false;

            _droppedItemsAttractor = attractor;
            _animation.AnimateOnHit();

            _currentHits++;

            if (_currentHits < _resourceData.HitsToGather) return false;
            
            _currentHits = 0;
            SpawnDropItem();
            
            if (!IsRemain())
            {
                Depleted?.Invoke();
                Destroy(gameObject);
            }
            
            return true;
        }

        public bool CanGather(ToolType toolType)
        {
            return toolType == _resourceData.ToolType && IsRemain();
        }

        private bool IsRemain()
        {
            return _spawnedCount < _resourceData.MaxQuantity;
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
    }
}