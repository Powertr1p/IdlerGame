using DefaultNamespace;
using UnityEngine;
using Zenject;

namespace Extraction
{
    public class ExtractionZone : MonoBehaviour
    {
        [SerializeField] private Transform _nextSpawnPoint;

        [Inject] private SignalBus _signalBus;

        public Transform NextSpawnPoint => _nextSpawnPoint;
        public bool HasNextIsland => !ReferenceEquals(_nextSpawnPoint, null);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IExitable _))
            {
                _signalBus.Fire(new ZoneEntered { Zone = this });
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IExitable _))
            {
                _signalBus.Fire(new ZoneExited { Zone = this });
            }
        }
    }
}
