using Inventory;
using Inventory.Core;
using Zenject;

namespace Utilities
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerInventory>().FromComponentInHierarchy().AsSingle();
        }
    }
}