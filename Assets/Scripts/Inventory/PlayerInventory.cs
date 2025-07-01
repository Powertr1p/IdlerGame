using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Core;
using UnityEngine;

namespace Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private ItemData _testItem;

        public event Action OnInitialized;
        public event Action<ItemData, int> OnResourceChanged;
        public Dictionary<ItemData, InventoryItem> Items => _items;
        
        private Dictionary<ItemData, InventoryItem> _items = new();

        private void Start()
        {
            //todo: тестирую, что спрайт подтягивается верно и количетсво
            
            OnInitialized?.Invoke();
            
            AddResource(_testItem, 15);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddResource(_testItem, 5);
                
                //todo: тестирую, что спрайт подтягивается верно и количетсво
            }
        }

        public void AddResource(ItemData item, int amount)
        {
            if (!_items.ContainsKey(item))
            {
                _items[item] = new InventoryItem(item, 0);
            }
            
            _items[item].Add(amount);
            
            OnResourceChanged?.Invoke(item, _items[item].Amount);
        }

        public bool RemoveResource(ItemData item, int amount)
        {
            if (!IsEnough(item, amount)) return false;
            
            _items[item].Remove(amount);
            OnResourceChanged?.Invoke(item, _items[item].Amount);
            return true;
        }
        
        public List<InventoryItem> GetAllItems()
        {
            return _items.Values.ToList();
        }
        
        private bool IsEnough(ItemData resource, int required)
        {
            return _items.ContainsKey(resource) && _items[resource].Amount >= required;
        }
    }
}