using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Core;
using UnityEngine;
using Utilities;

namespace UI.Views
{
    public class InventoryView : BaseView, IDisposable
    {
        [SerializeField] private InventorySlot _inventorySlotPrefab;
        [SerializeField] private Transform _inventoryContent;
        [SerializeField] private List<EquipmentSlot> _equipmentSlots;
        
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
        
        public void DisplayEquippedItem(InventoryItemDisplay item)
        {
            _equipmentSlots.FirstOrDefault(slot => slot.Type == item.ItemData.SlotType)?.EquipItem(item);
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
                Destroy(slot.gameObject);
            }
            
            foreach (var slot in _equipmentSlots)
            {
                slot.Clear();
            }
            
            _activeSlots.Clear();
        }
        
        private void HandleSlotClicked(InventoryItemDisplay item)
        {
            SlotClicked?.Invoke(item);
        }
    }
}