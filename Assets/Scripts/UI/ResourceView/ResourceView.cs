using System.Collections.Generic;
using Inventory;
using Inventory.Core;
using UnityEngine;

namespace UI.ResourceView
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private ResourceElementDisplayer _resourceElementPrefab;
        [SerializeField] private PlayerInventory _inventory;

        private Dictionary<ItemData, ResourceElementDisplayer> _displayItems = new Dictionary<ItemData, ResourceElementDisplayer>();

        private void OnEnable()
        {
            _inventory.OnInitialized += Initialize;
            _inventory.OnResourceChanged += UpdateView;
        }

        private void OnDisable()
        {
            _inventory.OnInitialized -= Initialize;
            _inventory.OnResourceChanged -= UpdateView;
        }

        private void Initialize()
        {
            foreach (var item in _inventory.Items)
            {
                InstantiateElement(item.Key);
            }
        }

        private void UpdateView(ItemData itemData, int amount)
        {
            if (!_displayItems.ContainsKey(itemData))
            {
                InstantiateElement(itemData);
            }
            
            _displayItems[itemData].SetAmount(amount);
        }

        private void InstantiateElement(ItemData itemData)
        {
            var instance = Instantiate(_resourceElementPrefab, transform);
            instance.SetAmount(0);
            instance.SetIcon(itemData.Icon);
            _displayItems.Add(itemData, instance);
        }
    }
}