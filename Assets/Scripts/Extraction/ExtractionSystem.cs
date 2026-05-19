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
        [Inject] private RaidInventory _raidInventory;
        [Inject] private PlayerMovement _player;
        [Inject] private SignalBus _signalBus;

        [Inject] private RaidResultPresenter _raidResultPresenter;
        [Inject] private ExtractionTimer _extractionTimer;
        [Inject] private RaidLootBuffer _raidLootBuffer;
        [Inject] private SceneLoader _sceneLoader;

        private ExtractionZone _currentZone;

        private void OnEnable()
        {
            _signalBus.Subscribe<ZoneEntered>(HandleZoneEntered);
            _signalBus.Subscribe<ZoneExited>(HandleZoneExited);

            _extractionTimer.ExitCompleted += HandleExitCompleted;

            _raidResultPresenter.OnExitRaidClicked += ProceedToLobby;
            _raidResultPresenter.OnContinueRaidClicked += ProceedToNextIsland;
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<ZoneEntered>(HandleZoneEntered);
            _signalBus.Unsubscribe<ZoneExited>(HandleZoneExited);

            _extractionTimer.ExitCompleted -= HandleExitCompleted;

            _raidResultPresenter.OnExitRaidClicked -= ProceedToLobby;
            _raidResultPresenter.OnContinueRaidClicked -= ProceedToNextIsland;
        }

        private void HandleZoneEntered(ZoneEntered signal)
        {
            _currentZone = signal.Zone;
            _extractionTimer.StartTimer();
        }

        private void HandleZoneExited(ZoneExited signal)
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
            _player.TeleportTo(_currentZone.NextSpawnPoint.position);
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
