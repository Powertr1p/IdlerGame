using System;
using System.Collections.Generic;
using Inventory;
using Inventory.Core;
using Inventory.ResourceItems;
using ItemRepository;
using UnityEngine;
using Zenject;

namespace UI.Model
{
    public class InventoryModel : IDisposable
    {
        [Inject] private PlayerInventory _playerInventory;
        [Inject] private ItemsRepository _itemRepository;

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

        public IReadOnlyList<IInventoryItem> GetInventoryItems()
        {
            var items =  _playerInventory.GetAll();
            
            return items;
        }
        
        private void InventoryChanged()
        {
            OnInventoryChanged?.Invoke();
        }
    }
}