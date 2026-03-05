using Inventory.Core;
using Inventory.EquipmentItems;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Tool", menuName = "Create Tool Data", order = 0)]
    public class ToolData : EquipmentData
    {
        [SerializeField] private ToolType _type;
        
        public override InventorySlotType SlotType { get; } = InventorySlotType.Tool;
        public override int Id => (int)_type;
        
        public ToolType Type => _type;
    }
}