using System;
using Inventory.EquipmentItems;
using Inventory.ResourceItems;
using UnityEngine;

namespace ResourceItems.Core
{
    public interface IGatherable
    {
        event Action Depleted;
        Transform Transform { get; }
        ResourceType Type { get; }
        bool IsRightTool(ToolType toolType);
        bool TryGather(ToolType toolType, Transform attractor);
        void StopGather();
    }
}