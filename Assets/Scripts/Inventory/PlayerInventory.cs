using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Inventory.Core;
using Inventory.EquipmentItems;
using Scriptable;
using UnityEngine;
using Utilities;
using Utilities.SaveSystem;
using Zenject;

namespace Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private EquipmentRepository _equipmentRepository;
        [SerializeField] private EquipmentChanger _equipmentChanger;
        
        public event Action<InventoryItem> OnResourceChanged;
        public Dictionary<ItemType, InventoryItem> Resources => _resources;
        
        private IPlayerLoadout _loadout;
        
        private Dictionary<ItemType, InventoryItem> _resources = new();
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
            ChangeTool();
        }
        
        [Inject]
        public void Construct(IPlayerLoadout loadout)
        {
            _loadout = loadout;
        }

        private void ChangeTool()
        {
            //temp
            _equippedToolId = _equippedToolId switch
            {
                -1 => 1,
                1 => 2,
                2 => 1,
                _ => _equippedToolId
            };
            
            var equipment = _equipmentRepository.GetEquipment(_equippedToolId);
            
            _loadout.SetTool(equipment);
            
            Equip(equipment);
        }

        private void Equip(ToolData tool)
        {
            _equipmentChanger.ChangeTool(tool);
        }
        
        private void SaveInventory()
        {
            if (_saveBox != null)
            {
                _saveBox.SaveInventory(_resources);
            }
        }

        private void LoadInventory()
        {
            if (_saveBox != null)
            {
                var loadedItems = _saveBox.LoadInventory();
                
                foreach (var item in loadedItems)
                { 
                    Add(item.Key, item.Value);
                }
            }
        }
        

        public int GetAmount(ItemType type)
        { 
            return _resources.TryGetValue(type, out var item) ? item.Amount : 0; 
        }

        public void Add(ItemType type, int amount)
        {
            InventoryItem updatedItem;
            
            if (!_resources.TryGetValue(type, out var item))
            {
                updatedItem = new InventoryItem(type, amount);
                _resources[type] = updatedItem;
            }
            else
            {
                item.Add(amount);
                updatedItem = item;
            }
            
            SaveInventory();
            OnResourceChanged?.Invoke(updatedItem);
        }

        public bool TrySpend(ItemType type, int amount)
        {
            return _resources.TryGetValue(type, out var item) && item.TrySpend(amount);
        }

        public IReadOnlyList<InventoryItem> GetAll()
        {
            return _resources.Values.ToList();
        }
    }
}