using System;
using System.Collections.Generic;
using Inventory.Core;
using Inventory.EquipmentItems;
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
                case HelmetData helmet:
                    LoadoutData.HelmetData = helmet;
                    break;
            }
            
            OnLoadoutChanged?.Invoke();
        }

        public void Unequip(InventorySlotType type)
        {
            switch (type)
            {
                case InventorySlotType.Helmet:
                    LoadoutData.HelmetData = null;
                    break;
                case InventorySlotType.Tool:
                     LoadoutData.ToolData = null;
                    break;
            }
        }
        
        public IReadOnlyList<IEquippable> GetEquippedItems()
        {
            return new List<IEquippable> { LoadoutData.ToolData, LoadoutData.HelmetData };
        }

        public ToolType GetToolType()
        {
            return LoadoutData.ToolData.ToolType;
        }
        
        public ToolData GetToolData()
        {
            return LoadoutData.ToolData;
        }
    }
}