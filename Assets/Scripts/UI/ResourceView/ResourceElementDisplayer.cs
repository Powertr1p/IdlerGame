using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ResourceView
{
    public class ResourceElementDisplayer : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _amount;

        public void SetAmount(int amount)
        {
            _amount.text = amount.ToString();
        }

        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }
    }
}