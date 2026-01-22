using Inventory.Core;
using Inventory.EquipmentItems;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Helmet", menuName = "Create Helmet Data", order = 0)]
    public class HelmetData : EquipmentData
    {
        [SerializeField] private HelmetType _helmetType;
        
        public override InventorySlotType SlotType { get; } = InventorySlotType.Helmet;
        public override int Id => (int)_helmetType;
    }
}