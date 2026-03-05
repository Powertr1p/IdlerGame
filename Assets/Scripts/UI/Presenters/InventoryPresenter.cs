using System.Collections.Generic;
using System.Linq;
using Inventory.Core;
using UI.Model;
using UI.Views;
using UnityEngine;
using Zenject;

namespace UI.Presenters
{
    public class InventoryPresenter : BasePresenter<InventoryView>
    {
        private InventoryModel _model;
        
        private bool _isUpdateNeeded = true;
        private List<InventoryItemDisplay> _lastInventoryItems = new();
        
        [Inject]
        public InventoryPresenter(
            [Inject(Id = "InventoryView")]InventoryView viewPrefab,
            [Inject(Id = "uiRoot")]Transform uiRoot,
            InventoryModel model) : base(viewPrefab, uiRoot)
        {
            _model = model;
        }
        
        protected override void OnViewCreated()
        {
            _model.OnInventoryChanged += HandleInventoryChanged;
            View.SlotClicked += HandleSlotClicked;
            View.UnequipRequested += HandleUnequip;
        }

        protected override void OnViewDestroy()
        {
            _model.OnInventoryChanged -= HandleInventoryChanged;
            View.SlotClicked -= HandleSlotClicked;
            View.UnequipRequested -= HandleUnequip;
            
            _model.Dispose();
            View.Dispose();
        }
        
        public override void Show()
        {
            base.Show();
            if (!_isUpdateNeeded) return;
            
            View.CreateInventorySlots();
            UpdateView();
            _isUpdateNeeded = false;
        }
        
        private void HandleInventoryChanged()
        {
            if (View.IsVisible)
            {
                UpdateView();
            }
            else
            {
                _isUpdateNeeded = true;
            }
        }

        private void UpdateView()
        {
            var items = _model.GetInventoryItems();
            var equippedItems = _model.GetEquippedItems();
            
            View.UpdateInventorySlots(items);
            View.UpdateEquipmentSlots(equippedItems);
        }

        private void HandleSlotClicked(InventoryItemDisplay item)
        {
            var slotType = item.ItemData.SlotType;
            
            if (item.ItemData is IEquippable eq)
            {
                _model.EquipItem(eq);
                View.DisplayEquippedItem(item);
            }
        }

        private void HandleUnequip(InventorySlotType type)
        {
            var clickedItem = _model.GetEquippedItems().FirstOrDefault(x => x.ItemData.SlotType == type);
    
            if (clickedItem.ItemData is IEquippable eq)
            {
                _model.Unequip(eq);
            }
        }
    }
}