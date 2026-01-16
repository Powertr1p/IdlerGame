using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Scriptable
{
    public class EquipmentDataBase : ScriptableObject
    {
        [SerializeField] private AssetReferenceGameObject _prefabLevel;
        [SerializeField] private AssetReferenceGameObject _toolLobbyPrefab;
        [SerializeField] private int _itemId;
        
        public AssetReferenceGameObject ToolLevelPrefab => _prefabLevel;
        public AssetReferenceGameObject ToolLobbyPrefab => _toolLobbyPrefab;
        public int ItemId => _itemId;
    }
}