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
        }

        private void OnDisable()
        {
            _sceneLoader.OnSceneLoaded -= OnSceneWasLoaded;
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

        private void ShowInventory()
        {
            _inventoryPresenter.Show();
        }

        private void ShowLobby()
        {
            _inventoryPresenter.Hide();
        }
        
        private async UniTaskVoid HandleStartRaid()
        {
            await _sceneLoader.LoadSceneAsync(GAME_SCENE_NAME);
            _lobbyBackground.SetActive(false);
        }

        private void OnSceneWasLoaded()
        {
            _inventoryPresenter.Hide();
        }
    }
}