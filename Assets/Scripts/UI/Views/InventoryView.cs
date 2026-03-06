using System;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using Inventory.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class InventoryView : BaseView, IDisposable
    {
        [SerializeField] private InventorySlot _inventorySlotPrefab;
        [SerializeField] private Transform _inventoryContent;
        [SerializeField] private List<EquipmentSlot> _equipmentSlots;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private float _slotHeight = 100f;
        
        private VirtualizedInventoryList _virtualizedInventoryList;
        
        public event Action<InventoryItemDisplay> SlotClicked;
        public event Action<InventorySlotType> UnequipRequested;
        
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
            if (_virtualizedInventoryList != null) return;
            
            _virtualizedInventoryList = new VirtualizedInventoryList(
                _slotPool, 
                _scrollRect,
                _slotHeight
            );
            _virtualizedInventoryList.SlotClicked += HandleSlotClicked;
        }
        
        public void UpdateInventorySlots(IReadOnlyList<InventoryItemDisplay> items)
        {
            _virtualizedInventoryList.SetItems(items);
        }

        public void UpdateEquipmentSlots(IReadOnlyList<InventoryItemDisplay> items)
        {
            foreach (var slot in _equipmentSlots)
            {
                slot.Clear();
            }
            
            foreach (var item in items)
            {
                DisplayEquippedItem(item);
            }
        }
        
        public void DisplayEquippedItem(InventoryItemDisplay item)
        {
            _equipmentSlots.FirstOrDefault(slot => slot.Type == item.ItemData.SlotType)?.EquipItem(item);
        }

        public void Dispose()
        {
            if (_virtualizedInventoryList != null)
            {
                _virtualizedInventoryList.SlotClicked -= HandleSlotClicked;
                _virtualizedInventoryList.Dispose();
                _virtualizedInventoryList = null;
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