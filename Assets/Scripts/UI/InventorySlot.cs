using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private Image _resourceImage;
        [SerializeField] private TextMeshProUGUI _quantityText;
        
        private const float SLOPE = 5f / 24f;  
        private const float INTERCEPT = 153.3333f;
        
        public void Bind(Sprite icon, int qty)
        {
            _resourceImage.sprite = icon;
            _resourceImage.preserveAspect = true;

            RescaleIconSize();
            
            _quantityText.text = qty.ToString();
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
    }
}