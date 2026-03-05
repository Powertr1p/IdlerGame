using System;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using Inventory.Core;
using Inventory.EquipmentItems;
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
        [Inject] private IPlayerLoadout _playerLoadout;

        public event Action OnInventoryChanged;
        
        [Inject]
        private void Initialize()
        {
            _playerInventory.OnInventoryChanged += InventoryChanged;
            _playerLoadout.OnLoadoutChanged += InventoryChanged;
        }
        
        public void Dispose()
        {
            _playerInventory.OnInventoryChanged -= InventoryChanged;
            _playerLoadout.OnLoadoutChanged -= InventoryChanged;
        }

        public IReadOnlyList<InventoryItemDisplay> GetInventoryItems()
        {
            var items =  _playerInventory.GetAll();
            var displayItems = new List<InventoryItemDisplay>();
            
            foreach (var item in items)
            {
                if (item is EquipmentItem { IsEquipped: true }) continue;
                
                var itemData = ItemRegistry.GetCached(item.SlotType, item.Id);

                if (!ReferenceEquals(itemData, null))
                {
                    displayItems.Add(new InventoryItemDisplay(itemData, item.Amount));
                }
            }
            
            return displayItems;
        }

        public IReadOnlyList<InventoryItemDisplay> GetEquippedItems()
        {
            var equippedItems = _playerLoadout.GetEquippedItems();
            var displayItems = new List<InventoryItemDisplay>(equippedItems.Count);
    
            foreach (var item in equippedItems)
            {
                if (item is ItemData itemData)
                {
                    displayItems.Add(new InventoryItemDisplay(itemData, 1));
                }
            }
    
            return displayItems;
        }
        
        public void EquipItem(IEquippable eq)
        {
            _playerInventory.EquipItem(eq);
        }

        public void Unequip(IEquippable eq)
        {
            _playerInventory.UnequipItem(eq);
        }
        
        private void InventoryChanged()
        {
            OnInventoryChanged?.Invoke();
        }
    }
}