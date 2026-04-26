using System.Collections.Generic;
using AssetLoader;
using Cysharp.Threading.Tasks;
using Inventory;
using Inventory.Core;
using Scriptable;
using UnityEngine;
using Zenject;

public class EquipmentChanger : MonoBehaviour
{
    [SerializeField] private Transform _toolLobbyContainer;
    [SerializeField] private Transform _backpackContainer;
    [SerializeField] private Transform _magicLobbyContainer;
    
    private readonly Dictionary<InventorySlotType, EquipmentSlot> _equipmentSlots = new();
    
    private AssetsLoader _assetsLoader;
    private IPlayerLoadout _loadout;
        
    [Inject]
    public void Construct(AssetsLoader loader, IPlayerLoadout loadout)
    {
        _assetsLoader = loader;
        _loadout = loadout;

        InitializeSlots();
    }

    private void OnEnable()
    {
        _loadout.OnLoadoutChanged += ApplyLoadout;
    }
        
    private void OnDisable()
    {
        _loadout.OnLoadoutChanged -= ApplyLoadout;
    }
    
    private void InitializeSlots()
    {
        _equipmentSlots[InventorySlotType.Tool] = new EquipmentSlot
        {
            Container = _toolLobbyContainer
        };

        _equipmentSlots[InventorySlotType.Backpack] = new EquipmentSlot
        {
            Container = _backpackContainer
        };

        _equipmentSlots[InventorySlotType.Magic] = new EquipmentSlot
        {
            Container = _magicLobbyContainer
        };
    }
        
    private void ApplyLoadout()
    {
        var loadoutData = _loadout.LoadoutData;
        
        foreach (var slot in _equipmentSlots)
        {
            var slotType = slot.Key;
            var equipmentData = GetEquipmentFromLoadout(loadoutData, slotType);
            ChangeEquipmentIfNeeded(slotType, equipmentData);
        }
    }
    
    private void ChangeEquipmentIfNeeded(InventorySlotType slotType, EquipmentData newData)
    {
        if (!_equipmentSlots.TryGetValue(slotType, out var slot)) return;
        
        if (ReferenceEquals(slot.Data, newData)) return;
        
        slot.Data = newData;
        _ = ChangeEquipmentAsync(slot, newData);
    }

    private async UniTaskVoid ChangeEquipmentAsync(EquipmentSlot slot, EquipmentData data)
    {
        if (!ReferenceEquals(slot.Prefab, null))
        {
            Destroy(slot.Prefab);
            slot.Prefab = null;
        }
        
        if (ReferenceEquals(data, null)) return;
        
        var cancellationToken = this.GetCancellationTokenOnDestroy();
        var prefabReference = data.LobbyPrefab;
        
        slot.Prefab = await _assetsLoader.InstantiateGameObject(prefabReference, cancellationToken);

        if (!ReferenceEquals(slot.Prefab, null))
        {
            slot.Prefab.transform.SetParent(slot.Container, false);
        }
    }
    
    private EquipmentData GetEquipmentFromLoadout(PlayerLoadoutData loadoutData, InventorySlotType slotType)
    {
        return slotType switch
        {
            InventorySlotType.Tool => loadoutData.ToolData,
            InventorySlotType.Backpack => loadoutData.BackpackData,
            InventorySlotType.Magic => loadoutData.MagicData,
            _ => null
        };
    }
}