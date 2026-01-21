using UI.Factories;
using UnityEngine;

namespace UI.Presenters
{
    public abstract class BasePresenter<TView> where TView : BaseView
    {
        protected TView View { get; private set; }
        
        private readonly TView _viewPrefab;
        private readonly Transform _uiRoot;

        private bool _isInitialized;

        protected BasePresenter(TView viewPrefab, Transform uiRoot)
        {
            _viewPrefab = viewPrefab;
            _uiRoot = uiRoot;
        }
        
        public virtual void Show()
        {
            TryInitialize();
            View.Show();
        }
        
        public virtual void Hide()
        {
            View?.Hide();
        }
        
        public virtual void Dispose()
        {
            _isInitialized = false;

            OnViewDestroy();
            Object.Destroy(View.gameObject);
            View = null;
            _isInitialized = false;
        }

        private void TryInitialize()
        {
            if (_isInitialized) return;
            
            View = ViewFactory.Create(_viewPrefab, _uiRoot);
            OnViewCreated();
            _isInitialized = true;
        }

        protected abstract void OnViewCreated();
        protected abstract void OnViewDestroy();
    }
}