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
        [SerializeField] private Button _closeButton;
        
        public event Action OnCloseClicked;
        
        private ObjectPool<InventorySlot> _pool;
        private DisplayItemSlotList _list;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(HandleCloseClick);
        }
        
        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(HandleCloseClick);
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
        
        private void HandleCloseClick()
        {
            OnCloseClicked?.Invoke();
        }
    }
}