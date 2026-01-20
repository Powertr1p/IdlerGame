using Core;
using Inventory;
using Lobby;
using UI;
using UI.Factories;
using UI.Presenters;
using UI.Views;
using UnityEngine;
using Zenject;

namespace Utilities
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private NavbarView _navbarView;
        [SerializeField] private Transform _uiRoot;
        
        public override void InstallBindings()
        {
            ViewFactory.BindContainer(Container);
            
            Container.Bind<PlayerInventory>().FromComponentInHierarchy().AsSingle();
            Container.Bind<INavigationService>().To<LobbyNavigator>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Transform>().WithId("uiRoot").FromInstance(_uiRoot);
            
            //Presenters
            Container.Bind<NavbarPresenter>().AsSingle();
            
            //Views
            Container.Bind<NavbarView>().WithId("NavbarView").FromInstance(_navbarView);
        }
    }
}