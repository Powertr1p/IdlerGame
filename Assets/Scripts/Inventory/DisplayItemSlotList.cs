using System;
using System.Collections.Generic;
using Inventory.Core;
using UI;

namespace Inventory
{
    public class DisplayItemSlotList : IDisposable
    {
        public event Action<InventoryItemDisplay> SlotClicked;
        
        private readonly ObjectPool<InventorySlot> _slotPool;
        private readonly List<InventorySlot> _activeSlots = new();

        public DisplayItemSlotList(ObjectPool<InventorySlot> pool)
        {
            _slotPool = pool;
        }

        public void Add(InventoryItemDisplay item)
        {
            var instance = _slotPool.Get();
            instance.Bind(item);
            instance.OnSlotClicked += HandleSlotClicked;
            _activeSlots.Add(instance);
        }
        
        public void Dispose()
        {
            Clear();
        }

        public void Clear()
        {
            foreach (var slot in _activeSlots)
            {
                slot.OnSlotClicked -= HandleSlotClicked;
                slot.Dispose();
                _slotPool.Return(slot);
            }
            
            _activeSlots.Clear();
        }
        
        private void HandleSlotClicked(InventoryItemDisplay item)
        {
            SlotClicked?.Invoke(item);
        }
    }
}