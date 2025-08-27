using Inventory;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerLoadoutInitializer : MonoBehaviour
    {
        [SerializeField] private Transform _toolContainer;

        private IPlayerLoadout _loadout;
        
        //todo: register 
        public void Construct(IPlayerLoadout loadout)
        {
            _loadout = loadout;
        }

        public void SpawnTool()
        {
            var instance = Instantiate(_loadout.LoadoutData.ToolData, _toolContainer);
        }
    }
}