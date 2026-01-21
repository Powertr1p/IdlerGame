using Inventory.Core;
using UnityEngine;
using Utilities;

namespace UI
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
        
        public void DisplayItem(InventoryItem item, Sprite sprite)
        {
            var instance = _slotPool.Get();
            instance.Bind(sprite, item.Amount);
        }
        
        private void BackToLobby()
        {
            LobbyUIEventBus.RequestLobbyShow();
        }
    }
}