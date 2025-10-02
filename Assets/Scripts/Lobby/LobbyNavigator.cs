using UI;
using UI.NavbarView;
using UnityEngine;
using Utilities;

namespace Core
{
    public class LobbyNavigator : MonoBehaviour
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private LobbyView _lobbyView;
        [SerializeField] private MenuSceneView _menuSceneView;
        [SerializeField] private NavbarView _navbarView;

        private const string GAME_SCENE_NAME = "GameScene";

        private void OnEnable()
        {
            _navbarView.NavbarButtonClicked += ChangeView;
            _sceneLoader.OnSceneLoaded += OnSceneWasLoaded;

            LobbyUIEventBus.OnInventoryOpenRequested += ShowInventory;
            LobbyUIEventBus.OnLobbyShowRequested += ShowLobby;
        }

        private void OnDisable()
        {
            _navbarView.NavbarButtonClicked -= ChangeView;
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
        }

        private void ChangeView(NavbarButtonType type)
        {
            if (type == NavbarButtonType.Play)
            {
                HandleStartRaid();
            }
        }

        private void OnSceneWasLoaded()
        {
            _menuSceneView.Hide();
            _lobbyView.Hide();
            _inventoryView.Hide();
        }
    }
}