using GameItems;
using Inventory.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "Resource/Config")]
    public class ResourceData : ScriptableObject
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private DropResource _resourcePrefab;
        [SerializeField] private int _maxQuantity;
        [SerializeField] private int _hitsToGather;
        [SerializeField] private ToolType _toolType;

        public ItemType ItemType => _itemType;
        public DropResource ResourcePrefab => _resourcePrefab;
        public int MaxQuantity => _maxQuantity;
        public float HitsToGather => _hitsToGather;
        public ToolType ToolType => _toolType;
    }
}