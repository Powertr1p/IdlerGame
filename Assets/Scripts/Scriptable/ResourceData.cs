using Inventory.Core;
using Inventory.ResourceItems;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Data/ResourceData")]
    public class ResourceData : ItemData
    {
        [SerializeField] private ResourceType _resourceType;

        public override InventorySlotType SlotType => InventorySlotType.Resource;
        public override int Id => (int)_resourceType;

        public ResourceType ResourceType => _resourceType;
    }
}
