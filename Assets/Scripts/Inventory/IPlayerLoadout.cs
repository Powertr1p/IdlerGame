using Inventory.Core;
using Inventory.EquipmentItems;
using Scriptable;

namespace Inventory
{
    public interface IPlayerLoadout
    {
        public PlayerLoadoutData LoadoutData { get; }
        public void SetTool(ToolData toolId);
        public ToolType GetToolType();
    }
}