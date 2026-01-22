using System.Collections.Generic;
using System.Linq;
using Inventory.Core;
using Scriptable;
using UnityEngine;

namespace ItemRepository
{
    [CreateAssetMenu(menuName = "Data/ItemRepository")]
    public class ItemsRepository : ScriptableObject
    {
        [SerializeField] private List<ItemData> _items;

        private Dictionary<InventorySlotType, Dictionary<int, ItemData>> _lookupByType;
        
        public Sprite GetSprite(InventorySlotType slotType, int id)
        {
            return GetItem(slotType, id).Sprite;
        }

        public string GetItemName(InventorySlotType slotType, int id)
        {
            return GetItem(slotType, id).DisplayName;
        }

        public ItemData GetItem(InventorySlotType slotType, int id)
        {
            TryInitialize();

            foreach (var types in _lookupByType)
            {
                if (types.Key == slotType)
                {
                    return types.Value[id];
                }
            }
            
            return null;
        }

        private void TryInitialize()
        {
            if (_lookupByType != null) return;
            
            _lookupByType = new Dictionary<InventorySlotType, Dictionary<int, ItemData>>();
            
            foreach (var item in _items)
            {
                if (item == null) continue;
                
                if (!_lookupByType.ContainsKey(item.SlotType))
                {
                    _lookupByType[item.SlotType] = new Dictionary<int, ItemData>();
                }
                
                _lookupByType[item.SlotType][item.Id] = item;
            }
        }
    }
}