using Inventory.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EquipmentSlot : MonoBehaviour
    {
        [SerializeField] private InventorySlotType _type;
        [SerializeField] private Image _equipmentImage;
        
        public InventorySlotType Type => _type;

        public void EquipItem(InventoryItemDisplay item)
        {
            _equipmentImage.sprite = item.ItemData.Sprite;
            _equipmentImage.preserveAspect = true;
        }
        
        public void UnequipItem()
        {
            _equipmentImage.sprite = null;
        }

        public void Clear()
        {
            _equipmentImage.sprite = null;
        }
    }
}