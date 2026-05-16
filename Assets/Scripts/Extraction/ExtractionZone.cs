using System;
using DefaultNamespace;
using UnityEngine;

namespace Extraction
{
    public class ExtractionZone : MonoBehaviour
    {
        [SerializeField] private Transform _nextSpawnPoint;

        public Transform NextSpawnPoint => _nextSpawnPoint;
        public bool HasNextIsland => !ReferenceEquals(_nextSpawnPoint, null);

        public event Action<ExtractionZone> PlayerEntered;
        public event Action<ExtractionZone> PlayerExited;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IExitable _))
            {
                PlayerEntered?.Invoke(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IExitable _))
            {
                PlayerExited?.Invoke(this);
            }
        }
    }
}
