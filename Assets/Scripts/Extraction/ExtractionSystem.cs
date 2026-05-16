using Cysharp.Threading.Tasks;
using Inventory.RaidInventory;
using UI.Presenters;
using UnityEngine;
using Utilities;
using Zenject;

namespace Extraction
{
    public class ExtractionSystem : MonoBehaviour
    {
        [SerializeField] private RaidInventory _raidInventory;
        [SerializeField] private ExtractionZone[] _exitZones;
        [SerializeField] private Transform _player;

        [Inject] private RaidResultPresenter _raidResultPresenter;
        [Inject] private ExtractionTimer _extractionTimer;
        [Inject] private RaidLootBuffer _raidLootBuffer;
        [Inject] private SceneLoader _sceneLoader;

        private ExtractionZone _currentZone;

        private void OnEnable()
        {
            foreach (var zone in _exitZones)
            {
                zone.PlayerEntered += HandlePlayerEntered;
                zone.PlayerExited += HandlePlayerExited;
            }

            _extractionTimer.ExitCompleted += HandleExitCompleted;

            _raidResultPresenter.OnExitRaidClicked += ProceedToLobby;
            _raidResultPresenter.OnContinueRaidClicked += ProceedToNextIsland;
        }

        private void OnDisable()
        {
            foreach (var zone in _exitZones)
            {
                zone.PlayerEntered -= HandlePlayerEntered;
                zone.PlayerExited -= HandlePlayerExited;
            }

            _extractionTimer.ExitCompleted -= HandleExitCompleted;

            _raidResultPresenter.OnExitRaidClicked -= ProceedToLobby;
            _raidResultPresenter.OnContinueRaidClicked -= ProceedToNextIsland;
        }

        private void HandlePlayerEntered(ExtractionZone zone)
        {
            _currentZone = zone;
            _extractionTimer.StartTimer();
        }

        private void HandlePlayerExited(ExtractionZone zone)
        {
            _extractionTimer.Cancel();
        }

        private void HandleExitCompleted()
        {
            bool canContinue = _currentZone.HasNextIsland;
            _raidResultPresenter.Show();
            _raidResultPresenter.SetContinueAvailable(canContinue);
        }

        private void ProceedToLobby()
        {
            StoreLootToBuffer();
            HandleResultViewClose().Forget();
        }

        private void ProceedToNextIsland()
        {
            _player.position = _currentZone.NextSpawnPoint.position;
            _raidResultPresenter.Hide();
        }

        private void StoreLootToBuffer()
        {
            _raidLootBuffer.Store(_raidInventory.GetLootDTO());
        }

        private async UniTaskVoid HandleResultViewClose()
        {
            await _sceneLoader.UnloadCurrentAsync();
        }
    }
}
