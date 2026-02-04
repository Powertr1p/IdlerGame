using System.Collections.Generic;
using System.Linq;
using Inventory.Core;

namespace Inventory.RaidInventory
{
    public class RaidLootBuffer
    {
        public bool HasLoot => _loot != null && _loot.Count > 0;
        
        private List<InventoryItemDto> _loot;

        public void Store(IReadOnlyList<InventoryItemDto> loot)
        {
            _loot = loot?.ToList() ?? new List<InventoryItemDto>();
        }

        public IReadOnlyList<InventoryItemDto> Consume()
        {
            var result = _loot ?? new List<InventoryItemDto>();
            _loot = null;
            return result;
        }
    }
}