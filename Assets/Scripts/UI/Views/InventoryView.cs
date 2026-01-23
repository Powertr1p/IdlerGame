using Inventory.Core;
using Inventory.ResourceItems;
using Scriptable;
using UnityEngine;
using Utilities;

namespace UI.Views
{
    public class InventoryView : BaseView
    {
        [SerializeField] private InventorySlot _inventorySlotPrefab;
        [SerializeField] private Transform _inventoryContent;
        
        private ObjectPool<InventorySlot> _slotPool;
        
        public void CreateInventorySlots(int amount)
        {
            _slotPool = new ObjectPool<InventorySlot>(amount, _inventorySlotPrefab, _inventoryContent);
        }
        
        public void DisplayItem(InventoryItemDisplay item)
        {
            var instance = _slotPool.Get();
            instance.Bind(item);
        }
    }
}