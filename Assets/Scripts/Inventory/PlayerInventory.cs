using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Core;
using Inventory.EquipmentItems;
using Inventory.ResourceItems;
using ItemRepository;
using UnityEngine;
using Utilities.SaveSystem;
using Zenject;

namespace Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private EquipmentChanger _equipmentChanger;
        
        public event Action OnInventoryChanged;
        
        private IPlayerLoadout _loadout;
        
        private List<IInventoryItem> _items = new();
        
        private PlayerInventorySaveBox _saveBox;
        
        private async void Start()
        {
            _saveBox = new PlayerInventorySaveBox();

            await ItemRegistry.PreloadLobbyItemsAsync();
            
            LoadInventory();

            if (_items.Count == 0)
            {
                Add(new EquipmentItem(InventorySlotType.Tool, 0, false));
                Add(new EquipmentItem(InventorySlotType.Tool, 1, false));
                Add(new EquipmentItem(InventorySlotType.Backpack, 0, false));
            }
        }
        
        [Inject]
        public void Construct(IPlayerLoadout loadout)
        {
            _loadout = loadout;
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

        public void Remove(IInventoryItem item)
        {
            _items.Remove(item);
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

        public void EquipItem(IEquippable eq)
        {
            UnequipSlot(eq.SlotType);
    
            var eqItem = _items.OfType<EquipmentItem>().FirstOrDefault(i => i.SlotType == eq.SlotType && i.Id == eq.Id);
            if (eqItem != null)
            {
                eqItem.Equip();
                _loadout.Equip(eq);

                SaveInventory();
                OnInventoryChanged?.Invoke();
            }
        }
        
        public void UnequipItem(IEquippable eq)
        {
            UnequipSlot(eq.SlotType);
            SaveInventory();
            OnInventoryChanged?.Invoke();
        }

        public void AddFromDtoList(IEnumerable<InventoryItemDto> dtos)
        {
            foreach (var dto in dtos)
            {
                AddFromDto(dto);
            }
        }
        
        private void UnequipSlot(InventorySlotType slotType)
        {
            var currentEquipped = GetEquippedItem(slotType);
            if (currentEquipped != null)
            {
                currentEquipped.Unequip();
                _loadout.Unequip(slotType);
            }
        }
        
        private void AddFromDto(InventoryItemDto dto)
        {
            var item = CreateItemFromDto(dto);
            
            if (item == null) return;
            Add(item);
        }
        
        private EquipmentItem GetEquippedItem(InventorySlotType slotType)
        {
            return _items.OfType<EquipmentItem>().FirstOrDefault(i => i.SlotType == slotType && i.IsEquipped);
        }

        public IReadOnlyList<IInventoryItem> GetAll()
        {
            return _items.AsReadOnly();
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
            if (_saveBox == null) return;

            var loadedData = _saveBox.LoadInventory();
            
            foreach (var dto in loadedData.Items)
            {
                IInventoryItem item = CreateItemFromDto(dto);
                
                if (item != null)
                {
                    _items.Add(item);

                    if (dto.IsEquipped && item is EquipmentItem eqItem)
                    {
                        var itemData = ItemRegistry.GetCached(eqItem.SlotType, eqItem.Id);
                        if (itemData is IEquippable equippable)
                        {
                            _loadout.Equip(equippable);
                        }
                    }
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
                case InventorySlotType.Backpack:
                    return new EquipmentItem(dto.SlotType, dto.Id, dto.IsEquipped);
        
                default:
                    Debug.LogWarning($"Неизвестный SlotType: {dto.SlotType}");
                    return null;
            }
        }
    }
}