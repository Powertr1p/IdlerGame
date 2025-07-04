namespace Inventory.Core
{
    [System.Serializable]
    public class InventoryItem
    {
        public ItemType Type { get; }
        public int Amount { get; private set; }

        public InventoryItem(ItemType type, int amount)
        {
            Type = type;
            Amount = amount;
        }

        public void Add(int value)
        {
            Amount += value;
        }

        public bool TrySpend(int value)
        {
            if (Amount < value) return false;
            
            Amount -= value;
            return true;
        }
    }
}