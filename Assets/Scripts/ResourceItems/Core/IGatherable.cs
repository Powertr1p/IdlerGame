using System;
using Inventory.Core;
using UnityEngine;

namespace GameItems
{
    public interface IGatherable
    {
        event Action Depleted;
        Transform Transform { get; }
        ItemType Type { get; }
        bool CanGather(ToolType toolType);
        bool TryGather(ToolType toolType, Transform attractor);
        void StopGather();
    }
}