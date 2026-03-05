using ItemRepository;
using UnityEngine;

namespace LevelSystems
{
    public class RaidBootstrap : MonoBehaviour
    {
        private async void Start()
        {
            await ItemRegistry.PreloadLevelItemsAsync();
        }
    }
}
