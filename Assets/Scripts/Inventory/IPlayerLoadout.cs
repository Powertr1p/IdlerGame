using Inventory.Core;
using Scriptable;

namespace Inventory
{
    public interface IPlayerLoadout
    {
        PlayerLoadoutData LoadoutData { get; }
        void SetTool(ToolData toolId);
        ToolType GetToolType();
    }
}