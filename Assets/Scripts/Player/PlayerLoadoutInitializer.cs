using AssetLoader;
using Cysharp.Threading.Tasks;
using Inventory;
using UnityEngine;
using Zenject;

public class PlayerLoadoutInitializer : MonoBehaviour
{
    [SerializeField] private Transform _toolContainer;
    [SerializeField] private Transform _backpackContainer;

    private IPlayerLoadout _loadout;
    private AssetsLoader _assetsLoader;
        
    [Inject]
    public void Construct(IPlayerLoadout loadout, AssetsLoader loader)
    {
        _loadout = loadout;
        _assetsLoader = loader;
    }

    private async void Start()
    {
        await SpawnTool();
        await SpawnBackpack();
    }

    private async UniTask SpawnTool()
    {
        if (!ReferenceEquals(_loadout.LoadoutData.ToolData, null))
        {
            var cancellationToken = this.GetCancellationTokenOnDestroy();
            var toolInstance = await _assetsLoader.InstantiateGameObject(_loadout.LoadoutData.ToolData.LevelPrefab, cancellationToken);
            toolInstance.transform.SetParent(_toolContainer, false);
        }
    }

    private async UniTask SpawnBackpack()
    {
        if (!ReferenceEquals(_loadout.LoadoutData.BackpackData, null))
        {
            var cancellationToken = this.GetCancellationTokenOnDestroy();
            var instance = await _assetsLoader.InstantiateGameObject(_loadout.LoadoutData.BackpackData.LevelPrefab, cancellationToken);
            instance.transform.SetParent(_backpackContainer, false);
        }
    }
}