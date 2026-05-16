using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SlowReceiver : MonoBehaviour, ISlowable
    {
        private NavMeshAgent _navMeshAgent;
        private float _baseSpeed;
        private float _slowEndTime;
        private bool _isSlowed;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _baseSpeed = _navMeshAgent.speed;
        }

        private void Update()
        {
            if (!_isSlowed) return;
            if (Time.time < _slowEndTime) return;

            _navMeshAgent.speed = _baseSpeed;
            _isSlowed = false;
        }

        public void ApplySlow(float multiplier, float duration)
        {
            if (!_isSlowed)
            {
                _navMeshAgent.speed = _baseSpeed * multiplier;
                _isSlowed = true;
            }

            _slowEndTime = Time.time + duration;
        }
    }
}
