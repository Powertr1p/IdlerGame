using Inventory.Core;
using Inventory.EquipmentItems;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Backpack", menuName = "Create Backpack Data", order = 0)]
    public class BackpackData : EquipmentData
    {
        [SerializeField] private BackpackType _backpackType;
        [SerializeField] private int _capacity = 20;
        public BackpackType Type => _backpackType;
        public int Capacity => _capacity;
        
        public override InventorySlotType SlotType { get; } = InventorySlotType.Backpack;
        public override int Id => (int)_backpackType;
    }
}