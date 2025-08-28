using Scriptable;
using UnityEngine;

namespace DefaultNamespace
{
    public class EquipmentChanger : MonoBehaviour
    {
        [SerializeField] private Transform _toolLobbyContainer;
        [SerializeField] private Transform _toolLevelContainer;
        [SerializeField] private bool _isInGameScene;

        private GameObject _equippedTool;
        private ToolData _currentToolData;

        public void ChangeTool(ToolData tool)
        {
            //визуально вепа меняется, но почему-то не меняется ToolData.ToolType
            _currentToolData = tool;
            
            if (!ReferenceEquals(_equippedTool, null))
            {
                Destroy(_equippedTool);
            }

            _equippedTool = _isInGameScene
                ? Instantiate(tool.ToolLevelPrefab, _toolLevelContainer)
                : Instantiate(tool.ToolLobbyPrefab, _toolLobbyContainer);
        }

        public void SetGameSceneMode(Transform toolContainer)
        {
            _isInGameScene = true;
            _toolLevelContainer = toolContainer;
            
            if (_currentToolData != null)
            {
                ChangeTool(_currentToolData);
            }
        }
    }
}