using System;
using Inventory.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class InventorySlot : MonoBehaviour, IDisposable, IPointerClickHandler
    {
        [SerializeField] private Image _resourceImage;
        [SerializeField] private TextMeshProUGUI _quantityText;
        
        [SerializeField] private float _iconLeftOffset = 10f;
        [SerializeField] private float _iconRightOffset = -10f;
        [SerializeField] private float _iconTopOffset = -10f;
        [SerializeField] private float _iconBottomOffset = 10f;
        
        public event Action<InventoryItemDisplay> OnSlotClicked;
        
        private InventoryItemDisplay _item;
        
        public void Bind(InventoryItemDisplay item)
        {
            _item = item;
            _resourceImage.sprite = _item.ItemData.Sprite;
            _resourceImage.preserveAspect = true;

            RescaleIconSize();
            
            _quantityText.text = _item.Amount.ToString();
        }
        
        public void Dispose()
        {
        }

        private void RescaleIconSize()
        {
            var rt = _resourceImage.rectTransform;
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.anchoredPosition = Vector2.zero;

            rt.offsetMin = new Vector2(_iconLeftOffset, _iconBottomOffset);
            rt.offsetMax = new Vector2(_iconRightOffset, _iconTopOffset);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnSlotClicked?.Invoke(_item);
        }
    }
}