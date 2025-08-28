using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class LobbyView : MonoBehaviour
    {
        [SerializeField] private Button _changeToolButton;
        [SerializeField] private Button _inventoryButton;
        
        private void OnEnable()
        {
            _changeToolButton.onClick.AddListener(LobbyUIEventBus.ChangeTool);
            _inventoryButton.onClick.AddListener(LobbyUIEventBus.RequestInventoryOpen);
        }

        private void OnDisable()
        {
            _changeToolButton.onClick.RemoveAllListeners();
            _inventoryButton.onClick.RemoveAllListeners();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}