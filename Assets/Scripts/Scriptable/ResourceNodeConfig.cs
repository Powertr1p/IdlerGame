using System;
using Inventory.Core;
using Inventory.EquipmentItems;
using ResourceItems.Core;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "ResourceNodeConfig", menuName = "Data/ResourceNodeConfig")]
    public class ResourceNodeConfig : ScriptableObject
    {
        [SerializeField] private DropResource _dropPrefab;
        [SerializeField] private ToolType _toolType;
        [SerializeField] private int _maxQuantity = 10;
        [SerializeField] private int _hitsToDeplete = 5;
        [SerializeField] private int _hitsToGather = 1;
        [SerializeField] private QualityRollConfig _qualityRollConfig;
        [SerializeField] private ItemQualityConfig _qualityColorConfig;
        [SerializeField] private QualityDrop[] _drops;

        public DropResource DropPrefab => _dropPrefab;
        public ToolType ToolType => _toolType;
        public int MaxQuantity => _maxQuantity;
        public int HitsToDeplete => _hitsToDeplete;
        public int HitsToGather => _hitsToGather;
        public QualityRollConfig QualityRollConfig => _qualityRollConfig;
        public ItemQualityConfig QualityColorConfig => _qualityColorConfig;

        public ResourceData GetDrop(ItemQuality quality)
        {
            if (ReferenceEquals(_drops, null) || _drops.Length == 0) return null;

            foreach (var drop in _drops)
            {
                if (ReferenceEquals(drop.Item, null)) continue;
                if (drop.Item.Quality == quality) return drop.Item;
            }

            return _drops[0].Item;
        }

        [Serializable]
        private class QualityDrop
        {
            public ResourceData Item;
        }
    }
}
