using System;
using System.Collections.Generic;
using Inventory.Core;
using Inventory.EquipmentItems;
using Scriptable;

namespace Inventory
{
    public interface IPlayerLoadout
    {
        public PlayerLoadoutData LoadoutData { get; }
        public void Equip(IEquippable equipment);
        public void Unequip(InventorySlotType type);
        public int GetBackpackCapacity();
        public IReadOnlyList<IEquippable> GetEquippedItems();
        public event Action OnLoadoutChanged;
    }
}