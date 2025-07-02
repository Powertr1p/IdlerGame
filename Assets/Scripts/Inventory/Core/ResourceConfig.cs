using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.Core
{
    [CreateAssetMenu(menuName = "Inventory/ResourceConfig")]
    public class ResourceConfig : ScriptableObject
    {
        [SerializeField] private List<ResourceViewData> _resources;

        private Dictionary<ResourceType, ResourceViewData> _lookup;

        public ResourceViewData Get(ResourceType type)
        {
            _lookup ??= _resources.ToDictionary(r => r.Type, r => r);
            return _lookup.GetValueOrDefault(type);
        }
    }
    
    [Serializable]
    public class ResourceViewData
    {
        public ResourceType Type;
        public Sprite Icon;
        public string DisplayName;
    }
}