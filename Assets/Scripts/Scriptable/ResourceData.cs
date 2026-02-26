using GameItems;
using Inventory.Core;
using Inventory.EquipmentItems;
using Inventory.ResourceItems;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Data/ResourceData")]
    public class ResourceData : ItemData
    {
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private DropResource _resourcePrefab;
        [SerializeField] private int _maxQuantity;
        [SerializeField] private int _hitsToDeplete;
        [SerializeField] private int _hitsToGather;
        [SerializeField] private ToolType _toolType;
        
        public override InventorySlotType SlotType => InventorySlotType.Resource;
        public override int Id => (int)_resourceType;
        
        public ResourceType ResourceType => _resourceType;
        public DropResource ResourcePrefab => _resourcePrefab;
        public int MaxQuantity => _maxQuantity;
        public int HitsToDeplete => _hitsToDeplete;
        public int HitsToGather => _hitsToGather;
        public ToolType ToolType => _toolType;
    }
}