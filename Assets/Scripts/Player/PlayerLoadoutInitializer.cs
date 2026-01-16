using AssetLoader;
using Cysharp.Threading.Tasks;
using Inventory;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class PlayerLoadoutInitializer : MonoBehaviour
    {
        [SerializeField] private Transform _toolContainer;

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
        }

        private async UniTask SpawnTool()
        {
            if (_loadout != null)
            {
                var cancellationToken = this.GetCancellationTokenOnDestroy();
                var toolInstance = await _assetsLoader.InstantiateGameObject(_loadout.LoadoutData.ToolData.ToolLevelPrefab, cancellationToken);
                toolInstance.transform.SetParent(_toolContainer, false);
            }
        }
    }
}