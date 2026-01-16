using AssetLoader;
using Cysharp.Threading.Tasks;
using Scriptable;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class EquipmentChanger : MonoBehaviour
    {
        [SerializeField] private Transform _toolLobbyContainer;
        [SerializeField] private Transform _toolLevelContainer;
        [SerializeField] private bool _isInGameScene;

        private GameObject _equippedTool;
        private ToolData _currentToolData;
        private AssetsLoader _assetsLoader;
        
        [Inject]
        public void Construct(AssetsLoader loader)
        {
            _assetsLoader = loader;
        }

        public async void ChangeTool(ToolData tool)
        {
            _currentToolData = tool;
            
            if (!ReferenceEquals(_equippedTool, null))
            {
                Destroy(_equippedTool);
            }
            
            await ChangeToolAsync(tool);
        }

        private async UniTask ChangeToolAsync(ToolData tool)
        {
            var cancellationToken = this.GetCancellationTokenOnDestroy();
            
            _equippedTool = _isInGameScene
                ? await _assetsLoader.InstantiateGameObject(tool.ToolLevelPrefab, cancellationToken)
                : await _assetsLoader.InstantiateGameObject(tool.ToolLobbyPrefab, cancellationToken);

            if (!ReferenceEquals(_equippedTool, null))
            {
                _equippedTool.transform.SetParent(_isInGameScene ? _toolLevelContainer : _toolLobbyContainer, false);
            }
        }

        public void SetGameSceneMode(Transform toolContainer)
        {
            _isInGameScene = true;
            _toolLevelContainer = toolContainer;
            
            if (_currentToolData != null)
            {
                ChangeTool(_currentToolData);
            }
        }
    }
}