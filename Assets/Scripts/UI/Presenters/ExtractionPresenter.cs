using Extraction;
using UI.Views;

namespace UI.Presenters
{
    public class ExtractionPresenter
    {
        private readonly ExtractionView _view;
        
        public ExtractionPresenter(ExtractionZone exitZone, ExtractionTimer timer, ExtractionView view)
        {
            _view = view;
            
            exitZone.PlayerEntered += timer.StartTimer;
            exitZone.PlayerExited += timer.Cancel;
            
            timer.TimerUpdated += HandleExtractionTimerUpdated;
            timer.ExitStarted += HandleExtractionStarted;
            timer.ExitCanceled += HandleExtractionCanceled;
            timer.ExitCompleted += HandleExtractionSuccess;
        }
        
        private void HandleExtractionSuccess()
        {
            _view.ShowSuccessExit();
        }
        
        private void HandleExtractionCanceled()
        {
            _view.ShowCanceledExit();
        }
        
        private void HandleExtractionStarted()
        {
            _view.Show();
        }
        
        private void HandleExtractionTimerUpdated(float seconds)
        {
            _view.UpdateTimer(seconds);
        }
    }
}