using System;
using DefaultNamespace;
using UnityEngine;

namespace Extraction
{
    public class ExtractionZone : MonoBehaviour
    {
        [SerializeField] private Transform _nextSpawnPoint;

        public Transform NextSpawnPoint => _nextSpawnPoint;

        public event Action PlayerEntered;
        public event Action PlayerExited;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IExitable _))
            {
                PlayerEntered?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IExitable _))
            {
                PlayerExited?.Invoke();
            }
        }
    }
}
