using Core;
using UnityEngine;

namespace UI
{
    public class LobbyView : MonoBehaviour
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