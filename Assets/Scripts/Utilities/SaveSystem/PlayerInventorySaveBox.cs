using System;
using System.Collections.Generic;
using Inventory;
using Inventory.Core;
using SaveSystem;
using UnityEngine;
using Utilities.SaveBox;
using VContainer;

namespace Utilities.SaveSystem
{
    public class PlayerInventorySaveBox : IDisposable
    {
        private const string PREFIX = "inventory";
        
        [PlayerPrefs(PREFIX, "items")]
        public string InventoryItemsJson { get; set; }
        
        private PlayerInventory _playerInventory;
        private bool _isInitialized = false;
        
        [Inject] 
        public PlayerInventorySaveBox(PlayerInventory playerInventory)
        {
            _playerInventory = playerInventory;
        }
        
        public void Dispose()
        {
            SaveInventory();
        }
        
        private void OnInventoryInitialized()
        {
            if (!_isInitialized)
            {
                LoadInventory();
                _isInitialized = true;
            }
        }
        
        public void LoadInventory()
        {
            PlayerPrefsUtility.LoadAll(this);
            
            if (!string.IsNullOrEmpty(InventoryItemsJson))
            {
                try
                {
                    var inventoryData = JsonUtility.FromJson<InventoryData>(InventoryItemsJson);
                    
                    if (inventoryData != null && inventoryData.Items != null)
                    {
                        foreach (var itemData in inventoryData.Items)
                        {
                            var item = Resources.Load<ItemData>($"Items/{itemData.ItemId}");
                            
                            if (item != null)
                            {
                                _playerInventory.AddResource(item, itemData.Amount);
                            }
                            else
                            {
                                Debug.LogWarning($"Не удалось найти предмет с ID: {itemData.ItemId}");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"Ошибка при загрузке инвентаря: {e.Message}");
                }
            }
            else
            {
                Debug.Log("Сохраненный инвентарь не найден");
            }
        }
        
        public void SaveInventory()
        {
            try
            {
                var items = _playerInventory.GetAllItems();
                
                var saveItems = new List<SavedItemData>();
                
                foreach (var item in items)
                {
                    var savedItem = new SavedItemData
                    {
                        ItemId = item.Item.name,
                        Amount = item.Amount
                    };
                    
                    saveItems.Add(savedItem);
                }
                
                var inventoryData = new InventoryData
                {
                    Items = saveItems.ToArray()
                };
                
                InventoryItemsJson = JsonUtility.ToJson(inventoryData);
                
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
        public SavedItemData[] Items;
    }
    
    [Serializable]
    public class SavedItemData
    {
        public string ItemId;
        public int Amount;
    }
}