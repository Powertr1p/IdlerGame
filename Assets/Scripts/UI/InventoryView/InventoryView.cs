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
        
        private void BackToLobby()
        {
            LobbyUIEventBus.RequestLobbyShow();
        }
    }
}