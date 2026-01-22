using System;
using Inventory.Core;
using Inventory.EquipmentItems;
using Inventory.ResourceItems;
using UnityEngine;

namespace GameItems
{
    public interface IGatherable
    {
        event Action Depleted;
        Transform Transform { get; }
        ResourceType Type { get; }
        bool CanGather(ToolType toolType);
        bool TryGather(ToolType toolType, Transform attractor);
        void StopGather();
    }
}