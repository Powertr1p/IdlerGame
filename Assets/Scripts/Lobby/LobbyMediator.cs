using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Core
{
    public class LobbyMediator : MonoBehaviour
    {
        [SerializeField] private RaidStartHandler _raidStartHandler;
        [SerializeField] private SceneLoader _sceneLoader;

        public event Action RaidSceneLoaded;

        private void OnEnable()
        {
            _raidStartHandler.OnPlayClicked += HandleStartRaid;
            _sceneLoader.OnSceneLoaded += OnSceneWasLoaded;
        }

        private void OnDisable()
        {
            _raidStartHandler.OnPlayClicked -= HandleStartRaid;
            _sceneLoader.OnSceneLoaded -= OnSceneWasLoaded;
        }

        private void HandleStartRaid()
        {
            _sceneLoader.LoadSceneAsync("GameScene");
        }

        private void OnSceneWasLoaded()
        {
            RaidSceneLoaded?.Invoke();
        }
    }
}