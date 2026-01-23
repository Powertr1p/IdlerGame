using System;
using System.Collections.Generic;
using Inventory;
using Inventory.Core;
using Inventory.ResourceItems;
using ItemRepository;
using Scriptable;
using UnityEngine;
using Zenject;

namespace UI.Model
{
    public class InventoryModel : IDisposable
    {
        [Inject] private PlayerInventory _playerInventory;
        [Inject] private ItemsRepository _itemRepository;
        [Inject] private IPlayerLoadout _playerLoadout;

        public event Action OnInventoryChanged;
        
        [Inject]
        private void Initialize()
        {
            _playerInventory.OnInventoryChanged += InventoryChanged;
        }
        
        public void Dispose()
        {
            _playerInventory.OnInventoryChanged -= InventoryChanged;
        }
        
        public Sprite GetSprite(InventorySlotType slotType, int id)
        {
            return _itemRepository.GetItem(slotType, id).Sprite;
        }
        
        public int GetQty(ResourceType type)
        {
            return _playerInventory.GetResourceAmount(type);
        }

        public IReadOnlyList<InventoryItemDisplay> GetInventoryItems()
        {
            var items =  _playerInventory.GetAll();
            var displayItems = new List<InventoryItemDisplay>();
            
            foreach (var item in items)
            {
                var itemData = _itemRepository.GetItem(item.SlotType, item.Id);
                displayItems.Add(new InventoryItemDisplay(itemData, item.Amount));
            }
           
            
            return displayItems;
        }
        
        public void EquipTool(ToolData tool)
        {
            // _playerLoadout.SetTool();
        }
        
        private void InventoryChanged()
        {
            OnInventoryChanged?.Invoke();
        }
    }
}