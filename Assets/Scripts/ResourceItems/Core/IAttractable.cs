using Inventory.ResourceItems;
using UnityEngine;

namespace ResourceItems.Core
{
    public interface IAttractable
    {
        void Attract(Transform attractor);
        ResourceType Type { get; }
    }
}