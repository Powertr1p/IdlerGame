using Core;
using UI;
using UI.Factories;
using UI.Navbar;
using UI.Presenters;
using UI.Views;
using UnityEngine;
using Utilities;
using Zenject;

namespace Lobby
{
    public class LobbyNavigator : MonoBehaviour, INavigationService
    {
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private LobbyView _lobbyView;

        private const string GAME_SCENE_NAME = "GameScene";
        private SceneLoader _sceneLoader;
        
        private NavbarPresenter _navbarPresenter;
        private NavbarView _navbarView;

        [Inject]
        private void Construct(SceneLoader sceneLoader, NavbarPresenter navbarPresenter)
        {
            _sceneLoader = sceneLoader;
            _navbarPresenter = navbarPresenter;
        }
        
        private void OnEnable()
        {
            _sceneLoader.OnSceneLoaded += OnSceneWasLoaded;
        }

        private void OnDisable()
        {
            _sceneLoader.OnSceneLoaded -= OnSceneWasLoaded;
        }

        private void Start()
        {
            _navbarPresenter.Show();
        }
        
        public void Open(NavbarButtonType type)
        {
            switch (type)
            {
                case NavbarButtonType.Lobby:
                    ShowLobby();
                    break;
                case NavbarButtonType.Inventory:
                    ShowInventory();
                    break;
                case NavbarButtonType.Play:
                    HandleStartRaid();
                    break;
            }
        }

        public void ShowInventory()
        {
            _lobbyView.Hide();
            _inventoryView.Show();
        }

        public void ShowLobby()
        {
            _inventoryView.Hide();
            _lobbyView.Show();
        }
        
        private async void HandleStartRaid()
        {
            await _sceneLoader.LoadSceneAsync(GAME_SCENE_NAME);
        }

        private void OnSceneWasLoaded()
        {
            _lobbyView.Hide();
            _inventoryView.Hide();
        }
    }
}