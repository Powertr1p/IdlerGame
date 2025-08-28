using Core;
using UnityEngine;

namespace MenuSceneView
{
    public class MenuSceneView : MonoBehaviour
    {
        [SerializeField] private LobbyMediator _lobbyMediator;

        private void OnEnable()
        {
            _lobbyMediator.RaidSceneLoaded += Hide;
        }

        private void OnDisable()
        {
            _lobbyMediator.RaidSceneLoaded -= Hide;
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}