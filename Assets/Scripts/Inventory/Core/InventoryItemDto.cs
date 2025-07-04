using System;

namespace Inventory.Core
{
    [Serializable]
    public class InventoryItemDto
    {
        public ItemType Type { get; private set; }
        public int Amount { get; private set; }

        public InventoryItemDto(ItemType type, int amount)
        {
            Type = type;
            Amount = amount;
        }
    }
}