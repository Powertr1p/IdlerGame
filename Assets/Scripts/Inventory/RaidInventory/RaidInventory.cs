using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Core;
using Inventory.ResourceItems;
using UnityEngine;
using Zenject;

namespace Inventory.RaidInventory
{
    public class RaidInventory : MonoBehaviour
    {
        public event Action<ResourceType, int> OnResourceAdded;
        public event Action OnInventoryFull;
        
        private readonly Dictionary<ResourceType, int> _loot = new();
        private IPlayerLoadout _loadout;

        [Inject]
        public void Construct(IPlayerLoadout loadout)
        {
            _loadout = loadout;
        }

        public bool CanAdd()
        {
            int currentCount = _loot.Values.Sum();
            int capacity = _loadout.GetBackpackCapacity();
            
            return currentCount < capacity;
        }

        public bool TryAdd(ResourceType resource)
        {
            if (!CanAdd())
            {
                OnInventoryFull?.Invoke();
                return false;
            }

            if (!_loot.TryAdd(resource, 1))
            {
                _loot[resource] += 1;
            }
            
            OnResourceAdded?.Invoke(resource, _loot[resource]);
            return true;
        }

        public int GetCurrentCount()
        {
            return _loot.Values.Sum();
        }

        public int GetMaxCapacity()
        {
            return _loadout.GetBackpackCapacity();
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