using System;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using Inventory.Core;
using UnityEngine;

namespace UI.Views
{
    public class InventoryView : BaseView, IDisposable
    {
        [SerializeField] private InventorySlot _inventorySlotPrefab;
        [SerializeField] private Transform _inventoryContent;
        [SerializeField] private List<EquipmentSlot> _equipmentSlots;
        
        public event Action<InventoryItemDisplay> SlotClicked;
        public event Action<InventorySlotType> UnequipRequested;
        
        private DisplayItemSlotList _inventorySlotList;
        
        private ObjectPool<InventorySlot> _slotPool;
        
        private void Awake()
        {
            _slotPool = new ObjectPool<InventorySlot>(10, _inventorySlotPrefab, _inventoryContent);
        }

        private void OnEnable()
        {
            foreach (var slot in _equipmentSlots)
            {
                slot.UnequipClicked += HandleUnequip;
            }
        }

        private void OnDisable()
        {
            foreach (var slot in _equipmentSlots)
            {
                slot.UnequipClicked -= HandleUnequip;
            }
        }

        public void CreateInventorySlots()
        {
            if (_inventorySlotList != null) return;
            
            _inventorySlotList = new DisplayItemSlotList(_slotPool);
            _inventorySlotList.SlotClicked += HandleSlotClicked;
        }
        
        public void DisplayItem(InventoryItemDisplay item)
        {
            _inventorySlotList.Add(item);
        }
        
        public void DisplayEquippedItem(InventoryItemDisplay item)
        {
            _equipmentSlots.FirstOrDefault(slot => slot.Type == item.ItemData.SlotType)?.EquipItem(item);
        }

        public void Dispose()
        {
            if (_inventorySlotList != null)
            {
                _inventorySlotList.SlotClicked -= HandleSlotClicked;
                _inventorySlotList.Dispose();
                _inventorySlotList = null;
            }

            _inventorySlotPrefab?.Dispose();
        }

        public void Clear()
        {
            _inventorySlotList.Clear();
            
            foreach (var slot in _equipmentSlots)
            {
                slot.Clear();
            }
        }
        
        private void HandleSlotClicked(InventoryItemDisplay item)
        {
            SlotClicked?.Invoke(item);
        }
        
        private void HandleUnequip(InventorySlotType type)
        {
            UnequipRequested?.Invoke(type);
        }
    }
}