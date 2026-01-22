using Inventory.ResourceItems;

namespace Inventory.Core
{
    public class ItemModel
    {
        public ResourceType Type { get; }

        public ItemModel(ResourceType type)
        {
            Type = type;
        }
    }
}