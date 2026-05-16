using Inventory.Core;
using Inventory.EquipmentItems;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Magic", menuName = "Data/MagicData")]
    public class MagicData : EquipmentData
    {
        [SerializeField] private MagicType _type;

        public MagicType Type => _type;

        public override InventorySlotType SlotType { get; } = InventorySlotType.Magic;
        public override int Id => (int)_type;
    }
}
