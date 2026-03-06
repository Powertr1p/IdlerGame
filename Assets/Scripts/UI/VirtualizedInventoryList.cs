using System;
using System.Collections.Generic;
using Inventory.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class VirtualizedInventoryList : IDisposable
    {
        public event Action<InventoryItemDisplay> SlotClicked;
        
        private readonly ObjectPool<InventorySlot> _slotPool;
        private readonly ScrollRect _scrollRect;
        private readonly RectTransform _content;
        private readonly RectTransform _viewport;

        private List<InventoryItemDisplay> _allItems = new();
        private List<InventorySlot> _visibleSlots = new();

        private float _slotHeight;
        private int _visibleCount;
        private int _firstVisibleIndex;
        private int _bufferSize = 2;

        public VirtualizedInventoryList(ObjectPool<InventorySlot> pool, ScrollRect scrollRect, float slotHeight)
        {
            _slotPool = pool;
            _scrollRect = scrollRect;
            _content = scrollRect.content;
            _viewport = scrollRect.viewport;
            _slotHeight = slotHeight;

            CalculateVisibleCount();
            _scrollRect.onValueChanged.AddListener(OnScrollChanged);
        }

        public void SetItems(IReadOnlyList<InventoryItemDisplay> items)
        {
            _allItems = new List<InventoryItemDisplay>(items);
            
            _content.sizeDelta = new Vector2(_content.sizeDelta.x, _allItems.Count * _slotHeight);

            _firstVisibleIndex = -1;

            UpdateVisibleSlots();
        }

        public void Add(InventoryItemDisplay item)
        {
            _allItems.Add(item);
            
            _content.sizeDelta = new Vector2(_content.sizeDelta.x, _allItems.Count * _slotHeight);

            UpdateVisibleSlots();
        }

        public void Clear()
        {
            foreach (var slot in _visibleSlots)
            {
                slot.OnSlotClicked -= HandleSlotClicked;
                slot.Dispose();
                _slotPool.Return(slot);
            }
            
            _visibleSlots.Clear();
            _allItems.Clear();
            _content.sizeDelta = new Vector2(_content.sizeDelta.x, 0);
        }
        
        public void Dispose()
        {
            if (_scrollRect != null)
            {
                _scrollRect.onValueChanged.RemoveListener(OnScrollChanged);
            }
            Clear();
        }
        
        private void OnScrollChanged(Vector2 position)
        {
            UpdateVisibleSlots();
        }
        
        private void UpdateVisibleSlots()
        {
            CalculateVisibleRange(out int firstIndex, out int lastIndex);

            int requiredSlots = lastIndex - firstIndex + 1;
            if (firstIndex == _firstVisibleIndex && _visibleSlots.Count == requiredSlots) return;
            
            _firstVisibleIndex = firstIndex;
            

            while (_visibleSlots.Count > requiredSlots)
            {
                int  lastSlotIndex = _visibleSlots.Count - 1;
                var slot = _visibleSlots[lastSlotIndex];
                slot.OnSlotClicked -= HandleSlotClicked;
                slot.Dispose();
                _slotPool.Return(slot);
                _visibleSlots.RemoveAt(lastSlotIndex);
            }

            while (_visibleSlots.Count < requiredSlots)
            {
                var slot = _slotPool.Get();
                slot.OnSlotClicked += HandleSlotClicked;
                _visibleSlots.Add(slot);
            }

            for (int i = 0; i < _visibleSlots.Count; i++)
            {
                int index = firstIndex + i;

                if (index >= 0 && index < _allItems.Count)
                {
                    var slot = _visibleSlots[i];
                    var item = _allItems[index];
                    
                    slot.Bind(item);
                    slot.gameObject.SetActive(true);

                    var rectTransform = slot.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = new Vector2(0, -index * _slotHeight);
                }
            }
        }
        
        private void CalculateVisibleRange(out int firstIndex, out int lastIndex)
        {
            float contentY = _content.anchoredPosition.y;
            float viewportHeight = _viewport.rect.height;
            
            firstIndex = Mathf.Max(0, Mathf.FloorToInt(contentY / _slotHeight) - _bufferSize);
            lastIndex = Mathf.Min(
                _allItems.Count - 1, 
                Mathf.CeilToInt((contentY + viewportHeight) / _slotHeight) + _bufferSize
            );
        }
        
        private void CalculateVisibleCount()
        {
            float viewportHeight = _viewport.rect.height;
            _visibleCount = Mathf.CeilToInt(viewportHeight / _slotHeight) + _bufferSize * 2;
        }
        
        private void HandleSlotClicked(InventoryItemDisplay item)
        {
            SlotClicked?.Invoke(item);
        }
    }
}