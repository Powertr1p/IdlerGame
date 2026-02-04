using Cysharp.Threading.Tasks;
using Inventory.RaidInventory;
using UI.Presenters;
using UI.Views;
using UnityEngine;
using Utilities;
using Zenject;

namespace Extraction
{
    public class ExtractionSystem : MonoBehaviour
    {
        [SerializeField] private ExtractionZone _exitZone;
        [SerializeField] private ExtractionView _view;
        [SerializeField] private RaidInventory _raidInventory;
        
        [Inject] private RaidLootBuffer _raidLootBuffer;
        [Inject] private SceneLoader _sceneLoader;
        
        private ExtractionTimer _extractionTimer;
        private ExtractionPresenter _extractionPresenter;

        private const float ExtractionDuration = 10f;
        
        private void Awake()
        {
            _extractionTimer = new ExtractionTimer(ExtractionDuration);
            _extractionPresenter = new ExtractionPresenter(_exitZone, _extractionTimer, _view);
        }

        private void OnEnable()
        {
            _extractionTimer.ExitCompleted += HandleExitCompleted;
        }
        
        private void OnDisable()
        {
            _extractionTimer.ExitCompleted -= HandleExitCompleted;
        }
        
        private void HandleExitCompleted()
        {
            ExitFlow().Forget();
        }

        private async UniTaskVoid ExitFlow()
        {
            _raidLootBuffer.Store(_raidInventory.GetLootDTO());
            await _sceneLoader.UnloadCurrentAsync();
        }
    }
}