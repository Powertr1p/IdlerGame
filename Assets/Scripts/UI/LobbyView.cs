using Core;
using UnityEngine;

namespace UI
{
    public class LobbyView : MonoBehaviour
    {
        [SerializeField] private RaidStartHandler _playButton;

        private void OnEnable()
        {
            _playButton.OnPlayClicked += Hide;
        }

        private void OnDisable()
        {
            _playButton.OnPlayClicked -= Hide;
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