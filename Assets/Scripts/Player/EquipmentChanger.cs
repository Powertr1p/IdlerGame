using Scriptable;
using UnityEngine;

namespace DefaultNamespace
{
    public class EquipmentChanger : MonoBehaviour
    {
        [SerializeField] private Transform _toolContainer;

        private GameObject _equippedTool;

        public void ChangeTool(ToolData tool)
        {
            if (!ReferenceEquals(_equippedTool, null))
            { 
                Destroy(_equippedTool);
            }
            
            _equippedTool = Instantiate(tool.ToolLobbyPrefab, _toolContainer);
        }
    }
}