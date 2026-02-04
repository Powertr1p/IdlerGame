using System;
using UI.Presenters;
using UI.Views;
using UnityEngine;

namespace Extraction
{
    public class ExtractionSystem : MonoBehaviour
    {
        [SerializeField] private ExtractionZone _exitZone;
        [SerializeField] private ExtractionView _view;
        
        private ExtractionTimer _extractionTimer;
        private ExtractionPresenter _extractionPresenter;

        private const float ExtractionDuration = 10f;

        private void Awake()
        {
            _extractionTimer = new ExtractionTimer(ExtractionDuration);
            _extractionPresenter = new ExtractionPresenter(_exitZone, _extractionTimer, _view);
        }

        private void OnEnable()
        {
            
        }
        
        private void OnDisable()
        {
            
        }
    }
}