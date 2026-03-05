using System.Collections.Generic;
using Inventory.Core;
using Inventory.RaidInventory;
using Inventory.ResourceItems;
using ItemRepository;
using UI.ResourceView;
using UnityEngine;
using Zenject;

namespace UI.ResourceDisplay
{
    public class ResourceDisplayer : MonoBehaviour
    {
        [SerializeField] private ResourceElementDisplayer _resourceElementPrefab;
        [SerializeField] private RaidInventory _raidInventory;

        private Dictionary<ResourceType, ResourceElementDisplayer> _displayItems = new Dictionary<ResourceType, ResourceElementDisplayer>();

        private void OnEnable()
        {
            _raidInventory.OnResourceAdded += UpdateView;
        }

        private void OnDisable()
        {
            _raidInventory.OnResourceAdded -= UpdateView;
        }
        
        private void UpdateView(ResourceType resource, int amount)
        {
            if (!_displayItems.ContainsKey(resource))
            {
                InstantiateElement(resource, amount);
            }
            
            _displayItems[resource].SetAmount(amount);
        }

        private void InstantiateElement(ResourceType resource, int amount)
        {
            var data = ItemRegistry.GetCached(InventorySlotType.Resource, (int)resource);
            ResourceElementDisplayer instance = Instantiate(_resourceElementPrefab, transform);
            
            instance.SetAmount(amount);
            instance.SetIcon(data.Sprite);
            _displayItems.Add(resource, instance);
        }
    }
}