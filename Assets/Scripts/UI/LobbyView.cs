using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class LobbyView : BaseView
    {
        [SerializeField] private Button _changeToolButton;
        
        private void OnEnable()
        {
            _changeToolButton.onClick.AddListener(LobbyUIEventBus.ChangeTool);
        }

        private void OnDisable()
        {
            _changeToolButton.onClick.RemoveAllListeners();
        }
    }
}