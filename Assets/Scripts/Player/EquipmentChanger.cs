using AssetLoader;
using Cysharp.Threading.Tasks;
using Inventory;
using Scriptable;
using UnityEngine;
using Zenject;

public class EquipmentChanger : MonoBehaviour
{
    [SerializeField] private Transform _toolLobbyContainer;
    [SerializeField] private Transform _toolLevelContainer;
    [SerializeField] private bool _isInGameScene;

    private GameObject _equippedTool;
    private ToolData _currentToolData;
    private AssetsLoader _assetsLoader;
    private IPlayerLoadout _loadout;
        
    [Inject]
    public void Construct(AssetsLoader loader, IPlayerLoadout loadout)
    {
        _assetsLoader = loader;
        _loadout = loadout;
    }

    private void OnEnable()
    {
        _loadout.OnLoadoutChanged += LoadoutChanged;
    }
        
    private void OnDisable()
    {
        _loadout.OnLoadoutChanged -= LoadoutChanged;
    }

    public void ChangeTool(ToolData tool)
    {
        _currentToolData = tool;
            
        if (!ReferenceEquals(_equippedTool, null))
        {
            Destroy(_equippedTool);
        }
            
        _ = ChangeToolAsync(tool);
    }

    private async UniTaskVoid ChangeToolAsync(ToolData tool)
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
        
    private void LoadoutChanged()
    {
        ChangeTool(_loadout.GetToolData());
    }
}