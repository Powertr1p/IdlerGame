using Inventory.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Scriptable
{
    public abstract class EquipmentData : ItemData, IEquippable
    {
        [SerializeField] private AssetReferenceGameObject _prefabLevel;
        [SerializeField] private AssetReferenceGameObject _prefabLobby;
        
        public AssetReferenceGameObject ToolLevelPrefab => _prefabLevel;
        public AssetReferenceGameObject ToolLobbyPrefab => _prefabLobby;
    }
}