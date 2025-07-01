namespace Inventory.Core
{
    [System.Serializable]
    public class InventoryItem
    {
        public ItemData Item => _item;
        public int Amount => _amount;
        
        private ItemData _item;
        private int _amount;

        public InventoryItem(ItemData item, int amount)
        {
            _item = item;
            _amount = amount;
        }

        public void Add(int amount)
        {
            _amount += amount;
        }

        public void Remove(int amount)
        {
            _amount -= amount;
        }
    }
}