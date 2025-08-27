using Inventory;
using Zenject;

namespace Utilities
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPlayerLoadout>().To<PlayerLoadout>().AsSingle().NonLazy();
        }
    }
}