using System.Collections.Generic;
using Inventory;
using Inventory.Core;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Zenject;

namespace UI
{
    public class InventoryView : BaseView
    {
        [SerializeField] private InventorySlot _inventorySlotPrefab;
        [SerializeField] private Transform _inventoryContent;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _inventoryButton;

        private PlayerInventory _playerInventory;
        private ItemsViewDatabase _itemsViewDatabase;
        private List<InventorySlot> _cachedSlots;
        
        private ObjectPool<InventorySlot> _slotPool;
        private int _initialPoolSize = 10;
        
        private void Awake()
        {
            _cachedSlots = new List<InventorySlot>();
            _slotPool = new ObjectPool<InventorySlot>(_initialPoolSize, _inventorySlotPrefab, _inventoryContent);
        }
        
        [Inject]
        public void Construct(PlayerInventory playerInventory, ItemsViewDatabase itemsViewDatabase)
        {
            _playerInventory = playerInventory;
            _itemsViewDatabase = itemsViewDatabase;
        }
        
        private void OnEnable()
        {
            _backButton.onClick.AddListener(BackToLobby);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveAllListeners();
        }

        public override void Show()
        {
            base.Show();

            if (_cachedSlots.Count > 0)
            {
                SyncSlotsWithInventory();
            }
            else
            {
                CreateInventorySlots();
            }
        }

        private void SyncSlotsWithInventory()
        {
            IReadOnlyList<InventoryItem> items = _playerInventory.GetAll();
            
            //todo: придумать простой синк, а дальше будем работать по событию изменения инвентаря
        }

        private void CreateInventorySlots()
        {
            IReadOnlyList<InventoryItem> items = _playerInventory.GetAll();
            
            int resourceQty;
            Sprite resourceSpr;
            
            for (int i = 0; i < items.Count; i++)
            {
                resourceSpr = _itemsViewDatabase.Get(items[i].Type).Icon;
                resourceQty = items[i].Amount;

                var instance = _slotPool.Get();
                instance.Bind(resourceSpr, resourceQty);
            }
        }
        
        private void BackToLobby()
        {
            LobbyUIEventBus.RequestLobbyShow();
        }
    }
}