using System;
using System.Collections.Generic;
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

        [Inject]private RaidResultPresenter _raidResultPresenter;
        [Inject] private ExtractionTimer _extractionTimer;

        [Inject]private RaidLootBuffer _raidLootBuffer;
        [Inject] private SceneLoader _sceneLoader;

        private readonly Dictionary<ExtractionZone, Action> _enterHandlers = new();
        private ExtractionZone _currentZone;

        private void OnEnable()
        {
            foreach (var zone in _exitZones)
            {
                ExtractionZone captured = zone;
                Action handler = () =>
                {
                    _currentZone = captured;
                    _extractionTimer.StartTimer();
                };
                _enterHandlers[zone] = handler;
                zone.PlayerEntered += handler;
                zone.PlayerExited += _extractionTimer.Cancel;
            }

            _extractionTimer.ExitCompleted += HandleExitCompleted;

            _raidResultPresenter.OnExitRaidClicked += ProceedToLobby;
            _raidResultPresenter.OnContinueRaidClicked += ProceedToNextIsland;
        }

        private void OnDisable()
        {
            foreach (var zone in _exitZones)
            {
                if (_enterHandlers.TryGetValue(zone, out var handler))
                {
                    zone.PlayerEntered -= handler;
                }
                zone.PlayerExited -= _extractionTimer.Cancel;
            }
            _enterHandlers.Clear();

            _extractionTimer.ExitCompleted -= HandleExitCompleted;

            _raidResultPresenter.OnExitRaidClicked -= ProceedToLobby;
            _raidResultPresenter.OnContinueRaidClicked -= ProceedToNextIsland;
        }

        private void HandleExitCompleted()
        {
            bool canContinue = !ReferenceEquals(_currentZone.NextSpawnPoint, null);
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