using System;
using UI.Model;
using UI.Views;
using UnityEngine;
using Zenject;

namespace UI.Presenters
{
    public class RaidResultPresenter : BasePresenter<RaidResultView>, IDisposable
    {
        public event Action OnExitRaidClicked;
        public event Action OnContinueRaidClicked;

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

        public void SetContinueAvailable(bool available)
        {
            View.SetContinueAvailable(available);
        }

        public override void Hide()
        {
            base.Hide();
        }

        protected override void OnViewCreated()
        {
            View.OnExitClicked += HandleExitClick;
            View.OnContinueRaidClicked += HandleContinueRaidClick;
        }

        protected override void OnViewDestroy()
        {
            View.OnExitClicked -= HandleExitClick;
            View.OnContinueRaidClicked -= HandleContinueRaidClick;
        }

        private void HandleExitClick()
        {
            OnExitRaidClicked?.Invoke();
        }

        private void HandleContinueRaidClick()
        {
            OnContinueRaidClicked?.Invoke();
        }
    }
}