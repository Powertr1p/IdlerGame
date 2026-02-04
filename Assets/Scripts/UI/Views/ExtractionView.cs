using System;
using TMPro;
using UnityEngine;

namespace UI.Views
{
    public class ExtractionView : BaseView, IDisposable
    {
        [SerializeField] private TextMeshProUGUI _timerLabel;

        public override void Show()
        {
            base.Show();
        }
        
        public void UpdateTimer(float seconds)
        {
            _timerLabel.text = Mathf.CeilToInt(seconds).ToString();
        }

        public void ShowSuccessExit()
        {
            Hide();
        }

        public void ShowCanceledExit()
        {
            Hide();
        }

        public void Dispose()
        {
            // TODO release managed resources here
        }
    }
}