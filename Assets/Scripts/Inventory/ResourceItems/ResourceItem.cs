using Inventory.Core;

namespace Inventory.ResourceItems
{
    [System.Serializable]
    public class ResourceItem : IInventoryItem
    {
        public InventorySlotType SlotType => InventorySlotType.Resource;
        public ResourceType Type { get; }
        public int Id => (int)Type;
        public int Amount { get; private set; }
        public ItemQuality Quality { get; }

        public ResourceItem(ResourceType type, int amount, ItemQuality quality = ItemQuality.Common)
        {
            Type = type;
            Amount = amount;
            Quality = quality;
        }
        
        public void Add(int value)
        {
            Amount += value;
        }

        public bool TrySpend(int value)
        {
            if (Amount < value) return false;
            
            Amount -= value;
            return true;
        }
    }
}