using UnityEngine;
using Utilities;

namespace Core
{
    public class LobbyMediator : MonoBehaviour
    {
        [SerializeField] private RaidStartHandler _raidStartHandler;
        [SerializeField] private SceneLoader _sceneLoader;

        private void OnEnable()
        {
            _raidStartHandler.OnPlayClicked += HandleStartRaid;
        }

        private void OnDisable()
        {
            _raidStartHandler.OnPlayClicked -= HandleStartRaid;
        }

        private void HandleStartRaid()
        {
            _sceneLoader.LoadSceneAsync("GameScene");
        }
    }
}