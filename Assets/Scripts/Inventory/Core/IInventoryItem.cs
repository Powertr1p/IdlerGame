namespace Inventory.Core
{
    public interface IInventoryItem
    {
        public InventorySlotType SlotType { get; }
        public int Id { get; }
        public int Amount { get; }
    }
}