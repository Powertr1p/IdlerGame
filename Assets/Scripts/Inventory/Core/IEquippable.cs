namespace Inventory.Core
{
    public interface IEquippable
    {
        InventorySlotType SlotType { get; }
        int Id { get; }
    }
}