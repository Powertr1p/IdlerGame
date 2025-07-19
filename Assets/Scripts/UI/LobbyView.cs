using Core;
using Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LobbyView : MonoBehaviour
    {
        [SerializeField] private LobbyMediator _lobbyMediator;
        [SerializeField] private Button _changeToolButton;
        [SerializeField] private PlayerInventory _playerInventory;

        private void OnEnable()
        {
            _changeToolButton.onClick.AddListener(ChangeTool);
            _lobbyMediator.RaidSceneLoaded += Hide;
        }

        private void ChangeTool()
        {
            _playerInventory.ChangeTool();
        }

        private void OnDisable()
        {
            _changeToolButton.onClick.RemoveAllListeners();
            _lobbyMediator.RaidSceneLoaded -= Hide;
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }
        
        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}