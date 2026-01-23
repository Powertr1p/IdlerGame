using System;
using System.Collections.Generic;
using Inventory.Core;
using UnityEngine;
using Utilities;

namespace UI.Views
{
    public class InventoryView : BaseView, IDisposable
    {
        [SerializeField] private InventorySlot _inventorySlotPrefab;
        [SerializeField] private Transform _inventoryContent;
        
        public event Action<InventoryItemDisplay> SlotClicked;
        
        private ObjectPool<InventorySlot> _slotPool;
        private List<InventorySlot> _activeSlots = new();
        
        public void CreateInventorySlots(int amount)
        {
            _slotPool = new ObjectPool<InventorySlot>(amount, _inventorySlotPrefab, _inventoryContent);
        }
        
        public void DisplayItem(InventoryItemDisplay item)
        {
            var instance = _slotPool.Get();
            instance.Bind(item);
            instance.OnSlotClicked += HandleSlotClicked;
            _activeSlots.Add(instance);
        }

        public void Dispose()
        {
            _inventorySlotPrefab?.Dispose();
            Clear();
        }

        public void Clear()
        {
            foreach (var slot in _activeSlots)
            {
                slot.OnSlotClicked -= HandleSlotClicked;
                slot.Dispose();
            }
            _activeSlots.Clear();
        }
        
        private void HandleSlotClicked(InventoryItemDisplay item)
        {
            SlotClicked?.Invoke(item);
        }
    }
}