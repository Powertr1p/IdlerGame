using System.Collections.Generic;
using Inventory;
using Inventory.Core;
using UnityEngine;
using Zenject;

namespace UI.Model
{
    public class InventoryModel
    {
        [Inject] public PlayerInventory PlayerInventory;
        [Inject] public ItemsViewDatabase ItemsViewDatabase;
        
        public Sprite GetSprite(ItemType type)
        {
            return ItemsViewDatabase.Get(type).Icon;
        }
        
        public int GetQty(ItemType type)
        {
            return PlayerInventory.GetAmount(type);
        }

        public IReadOnlyList<InventoryItem> GetInventoryItems()
        {
            return PlayerInventory.GetAll();
        }
    }
}