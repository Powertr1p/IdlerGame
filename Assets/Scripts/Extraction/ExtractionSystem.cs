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
        [SerializeField] private ExtractionZone _exitZone;
        
        [Inject]private RaidResultPresenter _raidResultPresenter;
        [Inject] private ExtractionTimer _extractionTimer;
        
        [Inject]private RaidLootBuffer _raidLootBuffer;
        [Inject] private SceneLoader _sceneLoader;
        
        private void OnEnable()
        {
            _exitZone.PlayerEntered += _extractionTimer.StartTimer;
            _exitZone.PlayerExited += _extractionTimer.Cancel;
            
            _extractionTimer.ExitCompleted += HandleExitCompleted;
            
            _raidResultPresenter.OnExitRaidClicked += ProceedToLobby;
        }
        
        private void OnDisable()
        {
            _exitZone.PlayerEntered -= _extractionTimer.StartTimer;
            _exitZone.PlayerExited -= _extractionTimer.Cancel;
            
            _extractionTimer.ExitCompleted -= HandleExitCompleted;
            
            _raidResultPresenter.OnExitRaidClicked -= ProceedToLobby;
        }
        
        private void HandleExitCompleted()
        {
            StoreLootToBuffer();
            _raidResultPresenter.Show();
        }

        private void ProceedToLobby()
        {
            HandleResultViewClose().Forget();
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