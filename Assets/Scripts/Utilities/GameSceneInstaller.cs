using DefaultNamespace;
using Extraction;
using Inventory.RaidInventory;
using UI.Model;
using UI.Presenters;
using UI.Views;
using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private Transform _uiRoot;
    [SerializeField] private RaidResultView _raidResultView;
    [SerializeField] private ExtractionView _extractionView;
    [SerializeField] private PlayerLoadoutInitializer _playerLoadoutInitializer;
    [SerializeField] private RaidInventory _raidInventory;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<ZoneEntered>();
        Container.DeclareSignal<ZoneExited>();

        Container.Bind<PlayerLoadoutInitializer>().FromInstance(_playerLoadoutInitializer).AsSingle();
        Container.Bind<RaidInventory>().FromInstance(_raidInventory).AsSingle();
        Container.Bind<PlayerMovement>().FromComponentInHierarchy().AsSingle();
        Container.Bind<FloatingJoystick>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Transform>().WithId("raidUiRoot").FromInstance(_uiRoot);
        Container.Bind<ExtractionTimer>().AsSingle();
        
        //Presenters
        Container.BindInterfacesAndSelfTo<RaidResultPresenter>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ExtractionPresenter>().AsSingle().NonLazy();
        
        //Views
        Container.Bind<RaidResultView>().WithId("RaidResultView").FromInstance(_raidResultView);
        Container.Bind<ExtractionView>().WithId("ExtractionView").FromInstance(_extractionView);
            
        //Models
        Container.Bind<RaidResultModel>().AsSingle();
    }
}