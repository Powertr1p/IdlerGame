using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.Core
{
    [CreateAssetMenu(menuName = "Inventory/ResourceConfig")]
    public class ItemConfig : ScriptableObject
    {
        [SerializeField] private List<ItemViewData> _resources;

        private Dictionary<ItemType, ItemViewData> _lookup;

        public ItemViewData Get(ItemType type)
        {
            _lookup ??= _resources.ToDictionary(r => r.Type, r => r);
            return _lookup.GetValueOrDefault(type);
        }
    }
    
    [Serializable]
    public class ItemViewData
    {
        public ItemType Type;
        public Sprite Icon;
        public string DisplayName;
    }
}