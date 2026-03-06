using System;
using TMPro;
using UnityEngine;

namespace UI.Views
{
    public class ExtractionView : BaseView, IDisposable
    {
        [SerializeField] private TextMeshProUGUI _timerLabel;
        
        public void UpdateTimer(float seconds)
        {
            _timerLabel.SetText("{0}",Mathf.CeilToInt(seconds));
        }

        public void Dispose()
        {
           
        }
    }
}