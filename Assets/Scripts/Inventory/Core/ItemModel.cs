namespace Inventory.Core
{
    public class ItemModel
    {
        public ItemType Type { get; }

        public ItemModel(ItemType type)
        {
            Type = type;
        }
    }
}