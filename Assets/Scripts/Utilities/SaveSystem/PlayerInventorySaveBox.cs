using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Core;
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

        public Dictionary<ResourceType, int> LoadInventory()
        {
            if (string.IsNullOrEmpty(InventoryItemsJson))
            {
                return new Dictionary<ResourceType, int>();
            }

            try
            {
                var inventoryData = JsonConvert.DeserializeObject<InventoryData>(InventoryItemsJson);

                if (inventoryData != null && inventoryData.Items != null && inventoryData.Items.Length > 0)
                {
                    Dictionary<ResourceType, int> loadedResources = new Dictionary<ResourceType, int>();

                    foreach (var itemDto in inventoryData.Items)
                    {
                        loadedResources[itemDto.Type] = itemDto.Amount;
                    }
                    
                    return loadedResources;
                }

                return new Dictionary<ResourceType, int>();
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка при загрузке инвентаря: {e.Message}");
                return new Dictionary<ResourceType, int>();
            }
        }

        public void SaveInventory(Dictionary<ResourceType, InventoryItem> items)
        {
            try
            {
                if (items == null || items.Count == 0)
                {
                    InventoryItemsJson = JsonConvert.SerializeObject(new InventoryData { Items = Array.Empty<InventoryItemDto>() });
                    PlayerPrefsUtility.SaveAll(this);
                    return;
                }
                
                var itemDtos = items.Values
                    .Where(item => item.Amount > 0)
                    .Select(item => new InventoryItemDto(item.Type, item.Amount))
                    .ToArray();

                var inventoryData = new InventoryData
                {
                    Items = itemDtos
                };

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