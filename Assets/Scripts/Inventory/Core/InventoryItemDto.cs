using System;
using Inventory.ResourceItems;

namespace Inventory.Core
{
    [Serializable]
    public class InventoryItemDto
    {
        public InventorySlotType SlotType { get; set; }
        public int Amount { get; set; }
        public int Id { get; set; }

        public InventoryItemDto(InventorySlotType type, int id, int amount)
        {
            SlotType = type;
            Id = id;
            Amount = amount;
        }
    }
}