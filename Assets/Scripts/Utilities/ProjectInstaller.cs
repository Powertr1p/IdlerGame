using Inventory;
using Inventory.Core;
using UnityEngine;
using Zenject;

namespace Utilities
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoader _sceneLoader;
        
        public override void InstallBindings()
        {
            Container.Bind<IPlayerLoadout>().To<PlayerLoadout>().AsSingle().NonLazy();
            Container.Bind<ItemsViewDatabase>().FromScriptableObjectResource("ItemsViewDatabase").AsSingle();
            Container.Bind<SceneLoader>().FromInstance(_sceneLoader).AsSingle();
        }
    }
}