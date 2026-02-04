using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Core;
using Inventory.ResourceItems;
using UnityEngine;

namespace Inventory.RaidInventory
{
    public class RaidInventory : MonoBehaviour
    {
        public event Action<ResourceType, int> OnResourceAdded;
        
        private readonly Dictionary<ResourceType, int> _loot = new();
        
        public void Add(ResourceType resource)
        {
            if (!_loot.TryAdd(resource, 1))
            {
                _loot[resource] += 1;
            }

            OnResourceAdded?.Invoke(resource, _loot[resource]);
        }

        public void Clear()
        {
            _loot.Clear();
        }
        
        public IReadOnlyList<InventoryItemDto> GetLootDTO()
        {
            return _loot.Select(x => new InventoryItemDto(
                InventorySlotType.Resource,
                (int)x.Key,
                x.Value,
                false
                )).ToList();
        }
    }
}