using AssetLoader;
using Inventory;
using ItemRepository;
using Zenject;

namespace Utilities
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPlayerLoadout>().To<PlayerLoadout>().AsSingle().NonLazy();
            Container.Bind<ItemsRepository>().FromScriptableObjectResource("ItemsRepository").AsSingle();
            Container.Bind<AssetsConfig>().FromScriptableObjectResource("AssetsConfig").AsSingle();
            Container.Bind<AssetsLoader>().AsSingle().NonLazy();
            Container.Bind<SceneLoader>().AsSingle().NonLazy();
        }
    }
}