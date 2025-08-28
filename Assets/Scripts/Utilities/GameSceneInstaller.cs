using DefaultNamespace;
using Inventory;
using Zenject;

namespace Utilities
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerLoadoutInitializer>().FromComponentInHierarchy().AsSingle();
        }
    }
}