using System;
using Extraction;
using UI.Views;
using UnityEngine;
using Zenject;

namespace UI.Presenters
{
    public class ExtractionPresenter : BasePresenter<ExtractionView>, IDisposable
    {
        private readonly ExtractionTimer _timer;
        
        [Inject]
        public ExtractionPresenter(
            [Inject(Id = "ExtractionView")] ExtractionView view, 
            [Inject(Id = "raidUiRoot")] Transform uiRoot, 
            ExtractionTimer timer) 
            : base(view, uiRoot)
        {
            _timer = timer;
            _timer.ExitStarted += Show;
        }
        
        protected override void OnViewCreated()
        {
            _timer.TimerUpdated += UpdateTimer;
            _timer.ExitCanceled += Hide;
            _timer.ExitCompleted += Hide;
        }

        protected override void OnViewDestroy()
        {
            _timer.TimerUpdated -= UpdateTimer;
            _timer.ExitCanceled -= Hide;
            _timer.ExitCompleted -= Hide;
        }
        
        private void UpdateTimer(float seconds)
        {
            View.UpdateTimer(seconds);
        }

        public override void Dispose()
        {
            base.Dispose();
            _timer.TimerUpdated -= UpdateTimer;
            _timer.ExitStarted -= Show;
            _timer.ExitCanceled -= Hide;
            _timer.ExitCompleted -= Hide;
        }
    }
}