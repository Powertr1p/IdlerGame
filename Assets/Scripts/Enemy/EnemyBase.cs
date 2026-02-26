using Enemy.StateMachine;
using ShareComponents;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class EnemyBase : MonoBehaviour
    {
        //todo: no player reference, monsterSystem will pass player
        [SerializeField] private Transform Target;
        [SerializeField] private DamageReceiver _damageReceiver;
        
        [Header("Ranges")]
        [SerializeField] private float _aggroRange;
        [SerializeField] private float _unAggroRange;
        [SerializeField] private float _attackRange = 1.5f;

        [Header("Movement")] 
        [SerializeField] private float _repathInterval = 0.2f;

        [Header("Combat")] 
        [SerializeField] private float _attackInterval = 1f;

        public bool IsAlive { get; private set; } = true;
        public bool HasTarget => !ReferenceEquals(Target, null);
        public Transform GetTarget => Target;

        protected bool IsRunning {get; private set;}
        
        private NavMeshAgent _navMeshAgent;
        private IEnemyState _state;

        private float _repathTimer;
        private float _attackTimer;

        private float SqrDistanceToTarget
        {
            get
            {
                if (!HasTarget) return float.PositiveInfinity;
                
                Vector3 delta = Target.position - transform.position;
                return delta.sqrMagnitude;
            }
        }
        
        protected virtual void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            ChangeState(EnemyStates.Idle);
        }
        
        protected virtual void Update()
        {
            if (_state == null) return;
            _state.Tick(this, Time.deltaTime);
        }

        public void ChangeState(IEnemyState next)
        {
            if (ReferenceEquals(next, null) || ReferenceEquals(_state, next)) return;
            
            _state?.Exit(this);
            _state = next;
            _state.Enter(this);
        }

        public void SetTarget(Transform target)
        {
            this.Target = target;
        }

        public bool IsInAggroRange()
        {
            return SqrDistanceToTarget <= _aggroRange * _aggroRange;
        }
        
        public bool IsOutOfUnAggroRange()
        {
            return SqrDistanceToTarget > _unAggroRange * _unAggroRange;
        }
        
        public bool IsInAttackRange()
        {
            return SqrDistanceToTarget <= _attackRange * _attackRange;
        }

        public void TryRepathToTarget()
        {
            if (!HasTarget) return;
            if (!_navMeshAgent.enabled) return;
            if (!_navMeshAgent.isOnNavMesh) return;

            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(Target.position);
        }
        
        public void StartChase()
        {
            IsRunning = true;
            PlayMovementAnimation();
            ResetRepathTimer();
            TryRepathToTarget();
        }

        public void StartIdle()
        {
            StopChase();
            PlayIdleAnimation();
        }

        public void StartAttack()
        {
            StopChase();
            PlayAttackAnimation();
            ResetAttackTimer();
        }

        public void TickRepath(float dt)
        {
            _repathTimer -= dt;
            if (_repathTimer <= 0f)
            {
                TryRepathToTarget();
                _repathTimer = _repathInterval;
            }
        }

        public void TickAttack(float dt)
        {
            _attackTimer -= dt;
            if (_attackTimer <= 0f)
            {
                DealDamage();
                _attackTimer = _attackInterval;
            }
        }

        public void Die()
        {
            if (!IsAlive) return;
            
            IsAlive = false;
            ChangeState(EnemyStates.Death);
        }

        public void StartDeath()
        {
            StopChase();
            DisableAgent();
            PlayDeathAnimation();
        }
        
        private void StopChase()
        {
            if (!_navMeshAgent.enabled) return;
            if (!_navMeshAgent.isOnNavMesh) return;
            
            _navMeshAgent.isStopped = true;
            _navMeshAgent.ResetPath();
            _navMeshAgent.velocity = Vector3.zero;
            _navMeshAgent.nextPosition = transform.position;
            IsRunning = false;
        }
        
        private void ResetRepathTimer()
        {
            _repathTimer = 0f;
        }
        
        private void DisableAgent()
        {
            if (!ReferenceEquals(_navMeshAgent, null))
            {
                _navMeshAgent.enabled = false;
            }
        }
        
        private void ResetAttackTimer()
        {
            _attackTimer = 0f;
        }
        
        protected abstract void DealDamage();
        protected abstract void PlayAttackAnimation();
        protected abstract void PlayDeathAnimation();
        protected abstract void PlayMovementAnimation();
        protected abstract void PlayIdleAnimation();
        
        private void OnValidate()
        {
            if (_aggroRange < 0f) _aggroRange = 0f;
            if (_unAggroRange < _aggroRange) _unAggroRange = _aggroRange + 0.5f;
            if (_attackRange < 0f) _attackRange = 0f;
            if (_attackRange > _aggroRange) _attackRange = _aggroRange;
            if (_repathInterval < 0.02f) _repathInterval = 0.02f;
            if (_attackInterval < 0.02f) _attackInterval = 0.02f;
        }
    }
}
