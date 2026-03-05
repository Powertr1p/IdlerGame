using System;
using Inventory.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class EquipmentSlot : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private InventorySlotType _type;
        [SerializeField] private Image _equipmentImage;
        
        public event Action<InventorySlotType> UnequipClicked;
        
        public InventorySlotType Type => _type;

        public void EquipItem(InventoryItemDisplay item)
        {
            _equipmentImage.sprite = item.ItemData.Sprite;
            _equipmentImage.preserveAspect = true;
        }

        public void Clear()
        {
            _equipmentImage.sprite = null;
        }
        
        private void UnequipItem()
        {
            _equipmentImage.sprite = null;
            UnequipClicked?.Invoke(_type);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
           UnequipItem();
        }
    }
}