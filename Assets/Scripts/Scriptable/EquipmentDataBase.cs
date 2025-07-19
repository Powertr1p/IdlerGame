using UnityEngine;

namespace Scriptable
{
    public class EquipmentDataBase : ScriptableObject
    {
        [SerializeField] private GameObject _toolLevelPrefab;
        [SerializeField] private GameObject _toolLobbyPrefab;
        [SerializeField] private int _itemId;
        
        public GameObject ToolLevelPrefab => _toolLevelPrefab;
        public GameObject ToolLobbyPrefab => _toolLobbyPrefab;
        public int ItemId => _itemId;
    }
}