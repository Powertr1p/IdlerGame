using Inventory;
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
        
        private ObjectPool<InventorySlot> _slotPool;
        private int _initialPoolSize = 10;
        
        private void Start()
        {
            _slotPool = new ObjectPool<InventorySlot>(_initialPoolSize, _inventorySlotPrefab, _inventoryContent);
        }
        
        [Inject]
        public void Construct(PlayerInventory playerInventory)
        {
            _playerInventory = playerInventory;
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
        }
        
        private void BackToLobby()
        {
            LobbyUIEventBus.RequestLobbyShow();
        }
    }
}