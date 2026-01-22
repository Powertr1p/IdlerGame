using System;
using System.Collections.Generic;
using Inventory.Core;
using Inventory.ResourceItems;
using UnityEngine;

namespace Inventory.RaidInventory
{
    public class RaidInventory : MonoBehaviour
    {
        public event Action<ResourceType, int> OnResourceAdded;
        
        private Dictionary<ResourceType, int> _resources = new();
        
        public void Add(ResourceType resource)
        {
            if (!_resources.TryAdd(resource, 1))
            {
                _resources[resource] += 1;
            }

            OnResourceAdded?.Invoke(resource, _resources[resource]);
        }
    }
}