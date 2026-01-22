using System;
using Inventory.ResourceItems;

namespace Inventory.Core
{
    [Serializable]
    public class InventoryItemDto
    {
        public InventorySlotType SlotType { get; private set; }
        public int Amount { get; private set; }
        public int Id { get; private set; }

        public InventoryItemDto(InventorySlotType type, int id, int amount)
        {
            SlotType = type;
            Id = id;
            Amount = amount;
        }
    }
}