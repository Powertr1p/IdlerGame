using System;
using System.Collections.Generic;
using Inventory;
using Inventory.Core;
using UnityEngine;
using Zenject;

namespace UI.Model
{
    public class InventoryModel : IDisposable
    {
        [Inject] private PlayerInventory _playerInventory;
        [Inject] private ItemsViewDatabase _itemsViewDatabase;

        public event Action OnInventoryChanged;
        
        [Inject]
        private void Initialize()
        {
            _playerInventory.OnResourceChanged += ResourceChanged;
        }
        
        public void Dispose()
        {
            _playerInventory.OnResourceChanged -= ResourceChanged;
        }
        
        public Sprite GetSprite(ItemType type)
        {
            return _itemsViewDatabase.Get(type).Icon;
        }
        
        public int GetQty(ItemType type)
        {
            return _playerInventory.GetAmount(type);
        }

        public IReadOnlyList<InventoryItem> GetInventoryItems()
        {
            return _playerInventory.GetAll();
        }
        
        private void ResourceChanged()
        {
            OnInventoryChanged?.Invoke();
        }
    }
}