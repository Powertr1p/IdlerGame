using UI;
using UnityEngine;
using Utilities;
using Zenject;

namespace Core
{
    public class LobbyNavigator : MonoBehaviour
    {
        [Inject] private SceneLoader _sceneLoader;
        
        [SerializeField] private RaidStartHandler _raidStartHandler;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private LobbyView _lobbyView;
        [SerializeField] private MenuSceneView _menuSceneView;

        private const string GAME_SCENE_NAME = "GameScene";

        private void OnEnable()
        {
            _raidStartHandler.OnPlayClicked += HandleStartRaid;
            _sceneLoader.OnSceneLoaded += OnSceneWasLoaded;

            LobbyUIEventBus.OnInventoryOpenRequested += ShowInventory;
            LobbyUIEventBus.OnLobbyShowRequested += ShowLobby;
        }

        private void OnDisable()
        {
            _raidStartHandler.OnPlayClicked -= HandleStartRaid;
            _sceneLoader.OnSceneLoaded -= OnSceneWasLoaded;
            
            LobbyUIEventBus.OnInventoryOpenRequested -= ShowInventory;
            LobbyUIEventBus.OnLobbyShowRequested -= ShowLobby;
        }

        private void HandleStartRaid()
        {
            _sceneLoader.LoadSceneAsync(GAME_SCENE_NAME);
        }

        private void ShowInventory()
        {
            _lobbyView.Hide();
            _inventoryView.Show();
        }

        private void ShowLobby()
        {
            _inventoryView.Hide();
            _lobbyView.Show();
            _menuSceneView.Show();
        }

        private void OnSceneWasLoaded()
        {
            _menuSceneView.Hide();
            _lobbyView.Hide();
            _inventoryView.Hide();
        }
    }
}