using System;
using UI.Model;
using UI.Views;
using UnityEngine;
using Zenject;

namespace UI.Presenters
{
    public class RaidResultPresenter : BasePresenter<RaidResultView>, IDisposable
    {
        public event Action OnCloseClicked;
        
        private readonly RaidResultModel _model;
        
        [Inject]
        public RaidResultPresenter(
            [Inject(Id = "RaidResultView")] RaidResultView view, 
            [Inject(Id = "raidUiRoot")] Transform uiRoot,
            RaidResultModel model) : base(view, uiRoot)
        {
           _model = model;
        }
        
        public override void Show()
        {
            base.Show();
            View.BindLoot(_model.GetLoot());
            View.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        protected override void OnViewCreated()
        {
            View.OnExitClicked += HandleExitClick;
        }

        protected override void OnViewDestroy()
        {
            View.OnExitClicked -= HandleExitClick;
        }
        
        private void HandleExitClick()
        {
            OnCloseClicked?.Invoke();
        }
    }
}