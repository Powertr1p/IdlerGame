using System;
using System.Collections.Generic;
using Inventory;
using Inventory.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class RaidResultView : BaseView
    {
        [SerializeField] private Transform _content;
        [SerializeField] private InventorySlot _itemSlotPrefab;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _continueRaidButton;
        
        public event Action OnExitClicked;
        public event Action OnContinueRaidClicked;
        
        private ObjectPool<InventorySlot> _pool;
        private DisplayItemSlotList _list;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(HandleExitClick);
            _continueRaidButton.onClick.AddListener(HandleContinueRaidClick);
        }
        
        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(HandleExitClick);
            _continueRaidButton.onClick.RemoveListener(HandleContinueRaidClick);
        }

        public void BindLoot(IEnumerable<InventoryItemDisplay> item)
        {
            _pool ??= new ObjectPool<InventorySlot>(10, _itemSlotPrefab, _content);
            _list ??= new DisplayItemSlotList(_pool);
            
            foreach (var i in item)
            {
                _list.Add(i);
            }
        }
        
        private void HandleExitClick()
        {
            OnExitClicked?.Invoke();
        }
        
        private void HandleContinueRaidClick()
        {
            OnContinueRaidClicked?.Invoke();
        }
    }
}