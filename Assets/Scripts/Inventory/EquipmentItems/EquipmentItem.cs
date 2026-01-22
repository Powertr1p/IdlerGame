using Inventory.Core;

namespace Inventory.EquipmentItems
{
    [System.Serializable]
    public class EquipmentItem : IInventoryItem
    {
        public InventorySlotType SlotType { get; private set; }
        public int Id { get; private set; }
        public int Amount { get; private set; } = 1;
        public bool IsEquipped { get; private set; } = false;
        
        public EquipmentItem(InventorySlotType slotType, int id, bool isEquipped)
        {
            SlotType = slotType;
            Id = id;
            IsEquipped = isEquipped;
        }

        public void Equip()
        {
            IsEquipped = true;
        }

        public void Unequip()
        {
            IsEquipped = false;
        }
    }
}