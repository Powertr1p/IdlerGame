using System;
using System.Collections.Generic;
using Inventory.Core;
using Scriptable;

namespace Inventory
{
    public class PlayerLoadout : IPlayerLoadout
    {
        public PlayerLoadoutData LoadoutData { get; } = new PlayerLoadoutData();
        
        public event Action OnLoadoutChanged;
        
        public void Equip(IEquippable equipment)
        {
            switch (equipment)
            {
                case ToolData tool:
                    LoadoutData.ToolData = tool;
                    break;
                case BackpackData backpack:
                    LoadoutData.BackpackData = backpack;
                    break;
            }
            
            OnLoadoutChanged?.Invoke();
        }

        public void Unequip(InventorySlotType type)
        {
            switch (type)
            {
                case InventorySlotType.Backpack:
                    LoadoutData.BackpackData = null;
                    break;
                case InventorySlotType.Tool:
                     LoadoutData.ToolData = null;
                    break;
            }
            
            OnLoadoutChanged?.Invoke();
        }
        
        public IReadOnlyList<IEquippable> GetEquippedItems()
        {
            return new List<IEquippable> { LoadoutData.ToolData, LoadoutData.BackpackData };
        }

        public int GetBackpackCapacity()
        {
            return LoadoutData.BackpackData?.Capacity ?? 0;
        }
    }
}