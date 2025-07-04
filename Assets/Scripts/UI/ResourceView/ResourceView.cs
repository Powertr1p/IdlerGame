using System.Collections.Generic;
using Inventory;
using Inventory.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.ResourceView
{
    public class ResourceView : MonoBehaviour
    {
        [FormerlySerializedAs("_resourceConfig")] 
        [SerializeField] private ItemConfig _itemConfig;
        [SerializeField] private ResourceElementDisplayer _resourceElementPrefab;
        [SerializeField] private PlayerInventory _inventory;

        private Dictionary<ItemType, ResourceElementDisplayer> _displayItems = new Dictionary<ItemType, ResourceElementDisplayer>();

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
            ItemViewData data = _itemConfig.Get(item.Type);
            ResourceElementDisplayer instance = Instantiate(_resourceElementPrefab, transform);
            
            instance.SetAmount(item.Amount);
            instance.SetIcon(data.Icon);
            _displayItems.Add(item.Type, instance);
        }
    }
}