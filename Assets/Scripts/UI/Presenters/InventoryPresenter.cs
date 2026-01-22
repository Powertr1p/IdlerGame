using UI.Model;
using UI.Views;
using UnityEngine;
using Zenject;

namespace UI.Presenters
{
    public class InventoryPresenter : BasePresenter<InventoryView>
    {
        private InventoryModel _model;
        
        private readonly int _initialInventorySlotsCount = 10;
        private bool _isUpdateNeeded = true;
        
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
        }

        protected override void OnViewDestroy()
        {
            _model.OnInventoryChanged -= HandleInventoryChanged;
            _model.Dispose();
        }
        
        public override void Show()
        {
            base.Show();
            if (!_isUpdateNeeded) return;
            
            View.CreateInventorySlots(_initialInventorySlotsCount);
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
            
            for (int i = 0; i < items.Count; i++)
            {
                var sprite = _model.GetSprite(items[i].SlotType, items[i].Id);
                
                View.DisplayItem(items[i], sprite);
            }
        }
    }
}