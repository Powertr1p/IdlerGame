using Inventory.Core;
using UnityEngine;

namespace Scriptable
{
    public abstract class ItemData : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _displayName;
        [SerializeField] private ItemQuality _quality = ItemQuality.Common;

        public Sprite Sprite => _sprite;
        public string DisplayName => _displayName;
        public ItemQuality Quality => _quality;

        public abstract InventorySlotType SlotType { get; }
        public abstract int Id { get; }
    }
}