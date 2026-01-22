using Inventory.Core;
using UnityEngine;

namespace Scriptable
{
    public abstract class ItemData : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _displayName;
        
        public Sprite Sprite => _sprite;
        public string DisplayName => _displayName;
        
        public abstract InventorySlotType SlotType { get; }
        public abstract int Id { get; }
    }
}