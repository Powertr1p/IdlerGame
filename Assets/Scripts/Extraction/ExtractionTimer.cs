using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Extraction
{
    public class ExtractionTimer
    {
        public event Action<float> TimerUpdated;
        public event Action ExitStarted;
        public event Action ExitCanceled;
        public event Action ExitCompleted;

        private CancellationTokenSource _cts;
        private readonly float _duration;
        
        public ExtractionTimer(float duration)
        {
            _duration = duration;
        }

        public void StartTimer()
        {
            _cts = new CancellationTokenSource();
            RunTimer(_cts.Token).Forget();
            ExitStarted?.Invoke();
        }

        public void Cancel()
        {
            _cts?.Cancel();
            ExitCanceled?.Invoke();
        }

        private async UniTaskVoid RunTimer(CancellationToken token)
        {
            float timeLeft = _duration;

            while (timeLeft > 0)
            {
                TimerUpdated?.Invoke(timeLeft);
                await UniTask.Delay(1000, cancellationToken: token);
                timeLeft--;
            }

            ExitCompleted?.Invoke();
        }
    }
}