using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Inventory.Core;
using Scriptable;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ItemRepository
{
    public static class ItemRegistry
    {
        private static readonly Dictionary<InventorySlotType, Dictionary<int, ItemData>> _cache = new();
        private static readonly HashSet<InventorySlotType> _loadedTypes = new();
        private static readonly Dictionary<InventorySlotType, UniTask> _loadingTasks = new();

        public static ItemData GetCached(InventorySlotType slotType, int id)
        {
            if (_cache.TryGetValue(slotType, out var typeDict))
            {
                if (typeDict.TryGetValue(id, out var item))
                {
                    return item;
                }
            }
            
            Debug.LogWarning($"ItemData не загружен: SlotType={slotType}, Id={id}. Используйте PreloadAsync.");
            return null;
        }

        public static async UniTask<ItemData> GetAsync(InventorySlotType slotType, int id)
        {
            await EnsureTypeLoadedAsync(slotType);
            return GetCached(slotType, id);
        }

        private static async UniTask PreloadAsync(InventorySlotType slotType)
        {
            await EnsureTypeLoadedAsync(slotType);
        }
        
        public static async UniTask PreloadLobbyItemsAsync()
        {
            await UniTask.WhenAll(
                PreloadAsync(InventorySlotType.Tool),
                PreloadAsync(InventorySlotType.Backpack),
                PreloadAsync(InventorySlotType.Resource)
            );
        }
        
        public static async UniTask PreloadLevelItemsAsync()
        {
            await PreloadAsync(InventorySlotType.Resource);
        }
        
        public static void UnloadType(InventorySlotType slotType)
        {
            if (_cache.ContainsKey(slotType))
            {
                _cache.Remove(slotType);
                _loadedTypes.Remove(slotType);
                Debug.Log($"ItemsRegistry: выгружен тип {slotType}");
            }
        }
        
        private static async UniTask EnsureTypeLoadedAsync(InventorySlotType slotType)
        {
            if (_loadedTypes.Contains(slotType)) return;
            
            if (_loadingTasks.TryGetValue(slotType, out var existingTask))
            {
                await existingTask;
                return;
            }
            
            var loadTask = LoadTypeAsync(slotType);
            _loadingTasks[slotType] = loadTask;

            try
            {
                await loadTask;
            }
            finally
            {
                _loadingTasks.Remove(slotType);
            }
        }
        
        private static async UniTask LoadTypeAsync(InventorySlotType slotType)
        {
            string label = GetLabelForSlotType(slotType);
            
            var handle = Addressables.LoadAssetsAsync<ItemData>(label, null);
            var items = await handle.Task;

            if (!_cache.ContainsKey(slotType))
            {
                _cache[slotType] = new Dictionary<int, ItemData>();
            }

            foreach (var item in items)
            {
                if (item != null && item.SlotType == slotType)
                {
                    _cache[slotType][item.Id] = item;
                }
            }

            _loadedTypes.Add(slotType);
            Debug.Log($"ItemsRegistry: загружено {items.Count} предметов типа {slotType}");
        }
        
        private static string GetLabelForSlotType(InventorySlotType slotType)
        {
            return slotType switch
            {
                InventorySlotType.Tool => "Tools",
                InventorySlotType.Backpack => "Backpacks",
                InventorySlotType.Resource => "Resources",
                _ => "Items"
            };
        }
    }
}