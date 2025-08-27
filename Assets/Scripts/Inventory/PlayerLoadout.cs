using Scriptable;

namespace Inventory
{
    public class PlayerLoadout : IPlayerLoadout
    {
        public PlayerLoadoutData LoadoutData { get; } = new PlayerLoadoutData();
        
        public void SetTool(ToolData tool)
        {
            LoadoutData.ToolData = tool;
        }
    }
}