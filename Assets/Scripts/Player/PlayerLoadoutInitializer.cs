using Inventory;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class PlayerLoadoutInitializer : MonoBehaviour
    {
        [SerializeField] private Transform _toolContainer;

        private IPlayerLoadout _loadout;
        
        [Inject]
        public void Construct(IPlayerLoadout loadout)
        {
            _loadout = loadout;
        }

        private void Start()
        {
            SpawnTool();
        }

        private void SpawnTool()
        {
            var instance = Instantiate(_loadout.LoadoutData.ToolData.ToolLevelPrefab, _toolContainer);
        }
    }
}