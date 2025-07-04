using System;
using DG.Tweening;
using Inventory.Core;
using JetBrains.Annotations;
using Scriptable;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameItems
{
    [RequireComponent(typeof(ResourceNodeAnimation))]
    public class ResourceNodeBase : MonoBehaviour, IGatherable
    { 
        [SerializeField] private ResourceData _resourceData;
        [SerializeField] private float _spreadRadius = 3f;
        
        [CanBeNull] private Transform _droppedItemsAttractor = null;
        
        public event Action Depleted;
        
        public Transform Transform { get; private set; }
        public ItemType Type { get; private set; }
        
        private ResourceNodeAnimation _animation;
        
        private int _currentHits;
        private int _spawnedCount;
        
        private void Awake()
        {
            Type = _resourceData.ItemType;
            _animation = GetComponent<ResourceNodeAnimation>();
            
            Transform = transform;
        }

        public bool TryGather(ToolType toolType, Transform attractor)
        {
            if (!CanGather(toolType)) return false;

            _droppedItemsAttractor = attractor;
            _animation.AnimateResourcePulse();

            _currentHits++;

            if (_currentHits < _resourceData.HitsToGather) return false;

            _currentHits = 0;
            SpawnDropItem();

            if (!CanSpawnDropItem())
            {
                Depleted?.Invoke();
                Destroy(gameObject);
            }

            return true;
        }

        public bool CanGather(ToolType toolType)
        {
            return toolType == _resourceData.ToolType && CanSpawnDropItem();
        }

        private bool CanSpawnDropItem()
        {
            return _spawnedCount < _resourceData.MaxQuantity;
        }

        private void SpawnDropItem()
        {
            if (!CanSpawnDropItem()) return;

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