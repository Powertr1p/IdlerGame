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
        
        public ResourceItem(ResourceType type, int amount)
        {
            Type = type;
            Amount = amount;
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