using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SlowDebuffHandler : MonoBehaviour, ISlowable
    {
        private NavMeshAgent _navMeshAgent;
        private float _activeMultiplier = 1f;
        private float _slowEndTime;
        private bool _isSlowed;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (!_isSlowed) return;
            if (Time.time < _slowEndTime) return;

            _navMeshAgent.speed /= _activeMultiplier;
            _activeMultiplier = 1f;
            _isSlowed = false;
        }

        public void ApplySlow(float multiplier, float duration)
        {
            if (!_isSlowed)
            {
                _activeMultiplier = multiplier;
                _navMeshAgent.speed *= multiplier;
                _isSlowed = true;
            }

            _slowEndTime = Time.time + duration;
        }
    }
}
