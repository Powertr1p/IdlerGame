using Core;
using Cysharp.Threading.Tasks;
using UI.Navbar;
using UI.Presenters;
using UnityEngine;
using Utilities;
using Zenject;

namespace Lobby
{
    public class LobbyNavigator : MonoBehaviour, INavigationService
    {
        [SerializeField] private GameObject _lobbyBackground;
        [SerializeField] private Camera _lobbyUICamera;
        
        private const string GAME_SCENE_NAME = "GameScene";
        
        private SceneLoader _sceneLoader;
        private InventoryPresenter _inventoryPresenter;

        [Inject]
        private void Construct(SceneLoader sceneLoader, InventoryPresenter inventoryPresenter)
        {
            _sceneLoader = sceneLoader;
            _inventoryPresenter = inventoryPresenter;
        }
        
        private void OnEnable()
        {
            _sceneLoader.OnSceneLoaded += OnSceneWasLoaded;
            _sceneLoader.OnSceneUnloaded += OnSceneWasUnloaded;
        }

        private void OnDisable()
        {
            _sceneLoader.OnSceneLoaded -= OnSceneWasLoaded;
            _sceneLoader.OnSceneUnloaded -= OnSceneWasUnloaded;
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
                    _ = HandleStartRaid();
                    break;
            }
        }

        private void ShowInventory()
        {
            _inventoryPresenter.Show();
        }

        private void ShowLobby()
        {
            _inventoryPresenter.Hide();
            _lobbyUICamera.enabled = true;
        }
        
        private async UniTaskVoid HandleStartRaid()
        {
            await _sceneLoader.LoadSceneAsync(GAME_SCENE_NAME);
            _lobbyBackground.SetActive(false);
            _lobbyUICamera.enabled = false;
        }

        private void OnSceneWasLoaded()
        {
            _inventoryPresenter.Hide();
        }
        
        private void OnSceneWasUnloaded()
        {
            _lobbyBackground.SetActive(true);
        }
    }
}