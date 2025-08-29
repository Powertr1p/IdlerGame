using System;
using System.Collections.Generic;
using Inventory.Core;
using UnityEngine;

namespace Inventory.RaidInventory
{
    public class RaidInventory : MonoBehaviour
    {
        public event Action<ItemType, int> OnResourceAdded;
        
        private Dictionary<ItemType, int> _resources = new();
        
        public void Add(ItemType item)
        {
            if (!_resources.TryAdd(item, 1))
            {
                _resources[item] += 1;
            }

            OnResourceAdded?.Invoke(item, _resources[item]);
        }
    }
}