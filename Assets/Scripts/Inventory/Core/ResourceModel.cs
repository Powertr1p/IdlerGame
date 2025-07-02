namespace Inventory.Core
{
    public class ResourceModel
    {
        public ResourceType Type { get; }

        public ResourceModel(ResourceType type)
        {
            Type = type;
        }
        
        public override string ToString() => Type.ToString();
    }
}