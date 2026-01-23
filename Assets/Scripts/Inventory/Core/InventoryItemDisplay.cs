using Scriptable;

namespace Inventory.Core
{
    public readonly struct InventoryItemDisplay
    {
        public ItemData ItemData { get; }
        public int Amount { get; }
        
        public InventoryItemDisplay(ItemData itemData, int amount)
        {
            ItemData = itemData;
            Amount = amount;
        }
    }
}