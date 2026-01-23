using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Inventory.Core;
using Inventory.EquipmentItems;
using Inventory.ResourceItems;
using ItemRepository;
using Scriptable;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;
using Utilities.SaveSystem;
using Zenject;

namespace Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private EquipmentChanger _equipmentChanger;
        
        public event Action OnInventoryChanged;
        
        private IPlayerLoadout _loadout;
        private ItemsRepository _itemRepository;
        
        private List<IInventoryItem> _items = new();
        
        private int _equippedToolId = -1;
        
        private PlayerInventorySaveBox _saveBox;

        private void OnEnable()
        {
            LobbyUIEventBus.OnChangeToolRequested += ChangeTool;
        }

        private void OnDisable()
        {
            LobbyUIEventBus.OnChangeToolRequested -= ChangeTool;
        }
        
        private void Start()
        {
            _saveBox = new PlayerInventorySaveBox();
            LoadInventory();

            if (_items.Capacity == 0)
            {
                Debug.Log("Item Added");
                Add(new EquipmentItem(InventorySlotType.Tool, 0, false));
            }
        }
        
        [Inject]
        public void Construct(IPlayerLoadout loadout, ItemsRepository itemsRepository)
        {
            _loadout = loadout;
            _itemRepository = itemsRepository;
        }
        
        public int GetResourceAmount(ResourceType type)
        {
            var resource = _items.OfType<ResourceItem>().FirstOrDefault(r => r.Type == type);

            return resource?.Amount ?? 0;
        }

        public void Add(IInventoryItem item)
        {
            if (item.SlotType == InventorySlotType.Resource)
            {
                var existing = _items.OfType<ResourceItem>().FirstOrDefault(r => r.Id == item.Id);
                if (existing != null)
                {
                    existing.Add(item.Amount);
                }
                else
                {
                    _items.Add(item);
                }
            }
            else
            {
                _items.Add(item);
            }
            
            SaveInventory();
            OnInventoryChanged?.Invoke();
        }

        public bool TrySpendResource(ResourceType type, int amount)
        {
            var resource = _items.OfType<ResourceItem>().FirstOrDefault(r => r.Type == type);
            if (resource != null && resource.TrySpend(amount))
            {
                SaveInventory();
                OnInventoryChanged?.Invoke();
                return true;
            }
            
            return false;
        }

        public IReadOnlyList<IInventoryItem> GetAll()
        {
            return _items.AsReadOnly();
        }
        
        private void ChangeTool()
        {
            //temp
            _equippedToolId = _equippedToolId switch
            {
                -1 => 0,
                0 => 1,
                1 => 0,
                _ => _equippedToolId
            };
            
            var equipment = _itemRepository.GetItem(InventorySlotType.Tool, _equippedToolId);
            
            var tool = equipment as ToolData;
            
            _loadout.SetTool(tool);
            Debug.Log($"Equipped tool: {_loadout.GetToolType()}");
            
            Equip(tool);
        }

        private void Equip(ToolData tool)
        {
            _equipmentChanger.ChangeTool(tool);
        }
        
        private void SaveInventory()
        {
            if (_saveBox != null)
            {
                _saveBox.SaveInventory(_items);
            }
        }

        private void LoadInventory()
        {
            LoadResources();
        }

        private void LoadResources()
        {
            if (_saveBox == null) return;

            var loadedData = _saveBox.LoadInventory();
            
            foreach (var dto in loadedData.Items)
            {
                IInventoryItem item = CreateItemFromDto(dto);
                
                if (item != null)
                {
                    _items.Add(item);
                }
            }
        }

        private IInventoryItem CreateItemFromDto(InventoryItemDto dto)
        {
            switch (dto.SlotType)
            {
                case InventorySlotType.Resource:
                    return new ResourceItem((ResourceType)dto.Id, dto.Amount);
        
                case InventorySlotType.Tool:
                case InventorySlotType.Helmet:
                    return new EquipmentItem(dto.SlotType, dto.Id, false);
        
                default:
                    Debug.LogWarning($"Неизвестный SlotType: {dto.SlotType}");
                    return null;
            }
        }
    }
}