using System;

namespace Inventory.Core
{
    [Serializable]
    public class InventoryItemDto
    {
        public InventorySlotType SlotType { get; set; }
        public int Amount { get; set; }
        public int Id { get; set; }
        public bool IsEquipped { get; set; }

        public InventoryItemDto(InventorySlotType type, int id, int amount, bool isEquipped)
        {
            SlotType = type;
            Id = id;
            Amount = amount;
            IsEquipped = isEquipped;
        }
    }
}