using System.Collections.Generic;
using Inventory;
using Inventory.Core;
using UnityEngine;

namespace UI.ResourceView
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private ResourceConfig _resourceConfig;
        [SerializeField] private ResourceElementDisplayer _resourceElementPrefab;
        [SerializeField] private PlayerInventory _inventory;

        private Dictionary<ResourceType, ResourceElementDisplayer> _displayItems = new Dictionary<ResourceType, ResourceElementDisplayer>();

        private void OnEnable()
        {
            _inventory.OnResourceChanged += UpdateView;
        }

        private void OnDisable()
        {
            _inventory.OnResourceChanged -= UpdateView;
        }
        
        private void UpdateView(InventoryItem item)
        {
            if (!_displayItems.ContainsKey(item.Type))
            {
                InstantiateElement(item);
            }
            
            _displayItems[item.Type].SetAmount(item.Amount);
        }

        private void InstantiateElement(InventoryItem item)
        {
            ResourceViewData data = _resourceConfig.Get(item.Type);
            ResourceElementDisplayer instance = Instantiate(_resourceElementPrefab, transform);
            
            instance.SetAmount(item.Amount);
            instance.SetIcon(data.Icon);
            _displayItems.Add(item.Type, instance);
        }
    }
}