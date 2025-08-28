using UnityEngine;
using Utilities;

namespace MenuSceneView
{
    public class MenuSceneView : MonoBehaviour
    {
        private void OnEnable()
        {
            LobbyUIEventBus.OnRaidStarted += Hide;
        }

        private void OnDisable()
        {
            LobbyUIEventBus.OnRaidStarted -= Hide;
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