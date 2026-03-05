using Inventory.Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Scriptable
{
    public abstract class EquipmentData : ItemData, IEquippable
    {
        [SerializeField] private AssetReferenceGameObject _prefabLevel;
        [SerializeField] private AssetReferenceGameObject _prefabLobby;
        
        public AssetReferenceGameObject LevelPrefab => _prefabLevel;
        public AssetReferenceGameObject LobbyPrefab => _prefabLobby;
    }
}