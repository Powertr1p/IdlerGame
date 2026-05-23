using System;
using Inventory.Core;
using Inventory.EquipmentItems;
using Inventory.ResourceItems;
using ResourceItems.Core;
using Scriptable;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameItems
{
    public class ResourceNode : MonoBehaviour, IGatherable
    {
        [SerializeField] private ResourceNodeConfig _nodeConfig;
        [SerializeField] private float _spreadRadius = 3f;
        [SerializeField] private ResourceNodeAnimationBase _animation;
        [SerializeField] private Transform _dropSpawnPoint;

        public event Action Depleted;

        public Transform Transform { get; private set; }
        public ResourceType Type { get; private set; }

        private int _currentHits;
        private int _spawnedCount;

        private void Awake()
        {
            Transform = transform;

            if (ReferenceEquals(_nodeConfig, null))
            {
                Debug.LogError($"ResourceNode '{name}' has no _nodeConfig assigned", this);
                return;
            }

            ResourceData common = _nodeConfig.GetDrop(ItemQuality.Common);
            if (!ReferenceEquals(common, null)) Type = common.ResourceType;
        }

        public bool TryGather(ToolType toolType)
        {
            _animation.AnimateOnHit();

            _currentHits++;

            if (_currentHits % GetNeededHitsToGather(toolType) != 0) return false;

            SpawnDropItem();

            if (!IsRemain(toolType))
            {
                DropRemainItems();
                Depleted?.Invoke();
                Destroy(gameObject);
            }

            return true;
        }

        public bool IsRightTool(ToolType toolType)
        {
            return toolType == _nodeConfig.ToolType && IsRemain(toolType);
        }

        public void StopGather()
        {
            _animation.KillSequence();
        }

        private bool IsRemain(ToolType tool)
        {
            return _currentHits < GetNeededHitsToDeplete(tool);
        }

        private void SpawnDropItem()
        {
            Vector3 startPosition = ReferenceEquals(_dropSpawnPoint, null)
                ? transform.position
                : _dropSpawnPoint.position;
            Vector2 randomDirection = Random.insideUnitCircle.normalized;

            float x = startPosition.x + randomDirection.x * _spreadRadius;
            float z = startPosition.z + randomDirection.y * _spreadRadius;

            Vector3 targetPosition = new Vector3(x, 0f, z);

            ItemQuality quality = ReferenceEquals(_nodeConfig.QualityRollConfig, null)
                ? ItemQuality.Common
                : _nodeConfig.QualityRollConfig.Roll();

            Color tint = ReferenceEquals(_nodeConfig.QualityColorConfig, null)
                ? Color.white
                : _nodeConfig.QualityColorConfig.GetColor(quality);

            ResourceData drop = _nodeConfig.GetDrop(quality);
            if (ReferenceEquals(drop, null)) return;

            DropResource dropItem = Instantiate(_nodeConfig.DropPrefab, startPosition, Quaternion.identity);
            dropItem.Initialize(startPosition, targetPosition, drop.ResourceType, quality, tint);

            _spawnedCount++;
        }

        private void DropRemainItems()
        {
            int remainCount = _nodeConfig.MaxQuantity - _spawnedCount;

            for (int i = 0; i < remainCount; i++)
            {
                SpawnDropItem();
            }
        }

        private int GetNeededHitsToGather(ToolType toolType)
        {
            return toolType == _nodeConfig.ToolType ? _nodeConfig.HitsToGather : _nodeConfig.HitsToGather * 2;
        }

        private int GetNeededHitsToDeplete(ToolType toolType)
        {
            return toolType == _nodeConfig.ToolType ? _nodeConfig.HitsToDeplete : _nodeConfig.HitsToDeplete * 2;
        }
    }
}
