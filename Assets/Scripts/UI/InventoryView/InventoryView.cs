using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private InventorySlot _inventorySlotPrefab;
        [SerializeField] private Transform _inventoryContent;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _inventoryButton;
        
        private void OnEnable()
        {
            _backButton.onClick.AddListener(BackToLobby);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveAllListeners();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        private void BackToLobby()
        {
            LobbyUIEventBus.RequestLobbyShow();
        }
    }
}