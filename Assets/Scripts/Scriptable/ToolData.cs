using Inventory.Core;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Tool", menuName = "Create Tool Data", order = 0)]
    public class ToolData : EquipmentDataBase
    {
        [SerializeField] private ToolType _toolType;
        
        public ToolType ToolType => _toolType;
    }
}