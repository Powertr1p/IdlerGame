using UI.Model;
using UnityEngine;
using Zenject;

namespace UI.Presenters
{
    public class InventoryPresenter : BasePresenter<InventoryView>
    {
        private InventoryModel _model;
        
        private int _initialInventorySlotsCount = 10;
        private bool _isFirstOpen = true;
        
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
        }

        protected override void OnViewDestroy()
        {
        }
        
        public override void Show()
        {
            base.Show();
            
            if (_isFirstOpen)
            {
                View.CreateInventorySlots(_initialInventorySlotsCount);
                UpdateView();
                _isFirstOpen = false;
            }
            else
            {
                UpdateView();
            }
        }

        private void SyncCachedWithInventory()
        {
            
        }

        private void UpdateView()
        {
            var items = _model.GetInventoryItems();
            
            for (int i = 0; i < items.Count; i++)
            {
                var sprite = _model.GetSprite(items[i].Type);
                View.DisplayItem(items[i], sprite);
            }
        }
    }
}