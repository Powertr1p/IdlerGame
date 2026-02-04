using Extraction;
using UI.Views;

namespace UI.Presenters
{
    public class ExtractionPresenter
    {
        public ExtractionPresenter(ExtractionZone exitZone, ExtractionTimer timer, ExtractionView view)
        {
            exitZone.PlayerEntered += timer.StartTimer;
            exitZone.PlayerExited += timer.Cancel;
            
            timer.TimerUpdated += view.UpdateTimer;
            timer.ExitStrated += view.Show;
            timer.ExitCanceled += view.ShowCanceledExit;
            timer.ExitCompleted += view.ShowSuccessExit;
        }
    }
}