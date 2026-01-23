using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Core;
using Inventory.EquipmentItems;
using Inventory.ResourceItems;
using Newtonsoft.Json;
using SaveSystem;
using UnityEngine;
using Utilities.SaveBox;

namespace Utilities.SaveSystem
{
    public class PlayerInventorySaveBox
    {
        private const string PREFIX = "inventory";

        [PlayerPrefs(PREFIX, "items")] 
        public string InventoryItemsJson { get; set; }

        public PlayerInventorySaveBox()
        {
            PlayerPrefsUtility.LoadAll(this);
        }

        public InventoryData LoadInventory()
        {
            if (string.IsNullOrEmpty(InventoryItemsJson))
            {
                return new InventoryData { Items = Array.Empty<InventoryItemDto>() };
            }

            try
            {
                var inventoryData = JsonConvert.DeserializeObject<InventoryData>(InventoryItemsJson);
                return inventoryData ?? new InventoryData { Items = Array.Empty<InventoryItemDto>() };
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка при загрузке инвентаря: {e.Message}");
                return new InventoryData { Items = Array.Empty<InventoryItemDto>() };
            }
        }

        public void SaveInventory(List<IInventoryItem> items)
        {
            try
            {
                if (items == null || items.Count == 0)
                {
                    InventoryItemsJson = JsonConvert.SerializeObject(
                        new InventoryData { Items = Array.Empty<InventoryItemDto>() });
                    PlayerPrefsUtility.SaveAll(this);
                    return;
                }

                var itemDtos = new List<InventoryItemDto>(items.Count);

                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    
                    Debug.Log(item.SlotType);
                    
                    itemDtos.Add(new InventoryItemDto(item.SlotType, item.Id, item.Amount));
                }
                
                var inventoryData = new InventoryData { Items = itemDtos.ToArray() };
                InventoryItemsJson = JsonConvert.SerializeObject(inventoryData);
                PlayerPrefsUtility.SaveAll(this);
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка при сохранении инвентаря: {e.Message}");
            }
        }
    }

    [Serializable]
    public class InventoryData
    {
        public InventoryItemDto[] Items;
    }
}