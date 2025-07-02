using System;

namespace Inventory.Core
{
    [Serializable]
    public class InventoryItemDto
    {
        public ResourceType Type { get; private set; }
        public int Amount { get; private set; }

        public InventoryItemDto(ResourceType type, int amount)
        {
            Type = type;
            Amount = amount;
        }
    }
}