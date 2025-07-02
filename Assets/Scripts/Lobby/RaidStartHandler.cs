using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class RaidStartHandler : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        public event Action OnPlayClicked;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveAllListeners();
        }

        private void OnPlayButtonClicked()
        {
            OnPlayClicked?.Invoke();
        }
    }
}