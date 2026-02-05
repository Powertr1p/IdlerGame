using System;
using Inventory.Core;
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
        
        public event Action<InventoryItemDisplay> OnSlotClicked;
        
        private InventoryItemDisplay _item;
        
        
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
            var rt = _resourceImage.rectTransform;
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.anchoredPosition = Vector2.zero;

            rt.offsetMin = new Vector2(10f, 10f);
            rt.offsetMax = new Vector2(-10f, -10f);
        }

        private void SlotClicked()
        {
            OnSlotClicked?.Invoke(_item);
        }
    }
}