using System;
using DefaultNamespace;
using UnityEngine;

namespace Extraction
{
    public class ExtractionZone : MonoBehaviour
    {
        public event Action PlayerEntered;
        public event Action PlayerExited;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IExitable exitable))
            {
                PlayerEntered?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IExitable exitable))
            {
                PlayerExited?.Invoke();
            }
        }
    }
}
