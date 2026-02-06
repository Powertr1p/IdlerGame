using System.Collections.Generic;
using System.Linq;
using Inventory.Core;
using Inventory.RaidInventory;
using ItemRepository;
using Zenject;

namespace UI.Model
{
    public class RaidResultModel
    {
        private readonly ItemsRepository _itemsRepository;
        private readonly RaidInventory _raidInventory;
        
        [Inject]
        public RaidResultModel(ItemsRepository itemsRepository, RaidInventory raidInventory)
        {
            _itemsRepository = itemsRepository;
            _raidInventory = raidInventory;
        }
        
        public List<InventoryItemDisplay> GetLoot()
        {
            var dtos = _raidInventory.GetLootDTO();
            var displays = dtos
                .Select(dto => new InventoryItemDisplay(_itemsRepository.GetItem(dto.SlotType, dto.Id), dto.Amount))
                .Where(x => x.ItemData != null)
                .ToList();
            
            return displays;
        }
    }
}