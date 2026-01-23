using System;
using Inventory.Core;
using Scriptable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventorySlot : MonoBehaviour, IDisposable
    {
        [SerializeField] private Image _resourceImage;
        [SerializeField] private TextMeshProUGUI _quantityText;
        [SerializeField] private Button _slotButton;

        private InventoryItemDisplay _item;
        
        public event Action OnSlotClicked;
        
        private const float SLOPE = 5f / 24f;  
        private const float INTERCEPT = 153.3333f;
        
        public void Bind(InventoryItemDisplay item)
        {
            _item = item;
            _resourceImage.sprite = _item.ItemData.Sprite;
            _resourceImage.preserveAspect = true;

            RescaleIconSize();
            
            _quantityText.text = _item.Amount.ToString();
            _slotButton.onClick.AddListener(SlotClicked);
        }
        
        public void Dispose()
        {
            _slotButton.onClick.RemoveListener(SlotClicked);
        }

        private void RescaleIconSize()
        {
            Vector2 spSizePx = _resourceImage.sprite.rect.size;
            float maxSide = Mathf.Max(spSizePx.x, spSizePx.y);
            
            if (maxSide <= 0.01f) return;

            float desiredSide = SLOPE * maxSide + INTERCEPT;
            float scale = desiredSide / maxSide;
            
            _resourceImage.rectTransform.sizeDelta = spSizePx * scale;
        }

        private void SlotClicked()
        {
            OnSlotClicked?.Invoke();
        }
    }
}