using Core;
using Inventory;
using Lobby;
using UI;

using UI.Factories;
using UI.Model;
using UI.Presenters;
using UI.Views;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private NavbarView _navbarView;
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private Transform _uiRoot;
        
    public override void InstallBindings()
    {
        ViewFactory.BindContainer(Container);
            
        Container.Bind<PlayerInventory>().FromComponentInHierarchy().AsSingle();
        Container.Bind<INavigationService>().To<LobbyNavigator>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Transform>().WithId("uiRoot").FromInstance(_uiRoot);
            
        //Models
        Container.Bind<InventoryModel>().AsSingle();
        
        //Presenters
        Container.Bind<NavbarPresenter>().AsSingle().NonLazy();
        Container.Bind<InventoryPresenter>().AsSingle();
            
        //Views
        Container.Bind<NavbarView>().WithId("NavbarView").FromInstance(_navbarView);
        Container.Bind<InventoryView>().WithId("InventoryView").FromInstance(_inventoryView);
    }
}