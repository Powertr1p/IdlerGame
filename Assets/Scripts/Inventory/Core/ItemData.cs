using UnityEngine;

namespace Inventory.Core
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Inventory/Resource")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private Sprite _icon;
        [SerializeField] private ToolType _requiredTool;
        
        public ResourceType ResourceType => _resourceType;
        public Sprite Icon => _icon;
        public ToolType RequiredTool => _requiredTool;
    }
}