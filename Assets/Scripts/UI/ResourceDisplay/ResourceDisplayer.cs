using System.Collections.Generic;
using Inventory.Core;
using Inventory.RaidInventory;
using UnityEngine;
using Zenject;

namespace UI.ResourceView
{
    public class ResourceDisplayer : MonoBehaviour
    {
        [Inject] private ItemsViewDatabase _itemsViewDatabase;
        [SerializeField] private ResourceElementDisplayer _resourceElementPrefab;
        [SerializeField] private RaidInventory _raidInventory;

        private Dictionary<ItemType, ResourceElementDisplayer> _displayItems = new Dictionary<ItemType, ResourceElementDisplayer>();

        private void OnEnable()
        {
            _raidInventory.OnResourceAdded += UpdateView;
        }

        private void OnDisable()
        {
            _raidInventory.OnResourceAdded -= UpdateView;
        }
        
        private void UpdateView(ItemType item, int amount)
        {
            if (!_displayItems.ContainsKey(item))
            {
                InstantiateElement(item, amount);
            }
            
            _displayItems[item].SetAmount(amount);
        }

        private void InstantiateElement(ItemType item, int amount)
        {
            ItemViewData data = _itemsViewDatabase.Get(item);
            ResourceElementDisplayer instance = Instantiate(_resourceElementPrefab, transform);
            
            instance.SetAmount(amount);
            instance.SetIcon(data.Icon);
            Debug.Log(instance);
            _displayItems.Add(item, instance);
        }
    }
}