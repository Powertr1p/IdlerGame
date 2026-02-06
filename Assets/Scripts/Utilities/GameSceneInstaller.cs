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
    [SerializeField] private ExtractionZone _extractionZone;
    
    public override void InstallBindings()
    {
        Container.Bind<PlayerLoadoutInitializer>().FromInstance(_playerLoadoutInitializer).AsSingle();
        Container.Bind<RaidInventory>().FromInstance(_raidInventory).AsSingle();
        Container.Bind<Transform>().WithId("raidUiRoot").FromInstance(_uiRoot);
        Container.Bind<ExtractionZone>().FromInstance(_extractionZone).AsSingle();
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