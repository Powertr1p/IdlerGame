using AssetLoader;
using Inventory;
using Inventory.Core;
using Zenject;

namespace Utilities
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPlayerLoadout>().To<PlayerLoadout>().AsSingle().NonLazy();
            Container.Bind<ItemsViewDatabase>().FromScriptableObjectResource("ItemsViewDatabase").AsSingle();
            Container.Bind<AssetsConfig>().FromScriptableObjectResource("AssetsConfig").AsSingle();
            Container.Bind<AssetsLoader>().AsSingle().NonLazy();
            Container.Bind<SceneLoader>().AsSingle().NonLazy();
        }
    }
}