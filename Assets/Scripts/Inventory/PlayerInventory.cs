using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Core;
using UnityEngine;
using Utilities.SaveSystem;

namespace Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        public event Action<InventoryItem> OnResourceChanged;

        public Dictionary<ItemType, InventoryItem> Items => _items;
        
        private Dictionary<ItemType, InventoryItem> _items = new();
        
        private PlayerInventorySaveBox _saveBox;

        private void Start()
        {
            _saveBox = new PlayerInventorySaveBox();
            LoadInventory();
        }
        
        private void SaveInventory()
        {
            if (_saveBox != null)
            {
                _saveBox.SaveInventory(_items);
            }
        }

        private void LoadInventory()
        {
            if (_saveBox != null)
            {
                var loadedItems = _saveBox.LoadInventory();
                
                foreach (var item in loadedItems)
                { 
                    Add(item.Key, item.Value);
                }
            }
        }
        

        public int GetAmount(ItemType type)
        { 
            return _items.TryGetValue(type, out var item) ? item.Amount : 0; 
        }

        public void Add(ItemType type, int amount)
        {
            InventoryItem updatedItem;
            
            if (!_items.TryGetValue(type, out var item))
            {
                updatedItem = new InventoryItem(type, amount);
                _items[type] = updatedItem;
            }
            else
            {
                item.Add(amount);
                updatedItem = item;
            }
            
            OnResourceChanged?.Invoke(updatedItem);
        }

        public bool TrySpend(ItemType type, int amount)
        {
            return _items.TryGetValue(type, out var item) && item.TrySpend(amount);
        }

        public IReadOnlyList<InventoryItem> GetAll()
        {
            return _items.Values.ToList();
        }
    }
}