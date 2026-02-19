namespace Enemy.StateMachine
{
    public sealed class ChaseState : IEnemyState
    {
        void IEnemyState.Enter(EnemyBase enemy)
        {
            enemy.StartChase();
        }

        void IEnemyState.Tick(EnemyBase enemy, float deltaTime)
        {
            if (!enemy.IsAlive)
            {
                enemy.ChangeState(EnemyStates.Death);
                return;
            }
            
            if (!enemy.HasTarget)
            {
                enemy.ChangeState(EnemyStates.Idle);
                return;
            }

            if (enemy.IsInAttackRange())
            {
                enemy.ChangeState(EnemyStates.Attack);
                return;
            }

            if (enemy.IsOutOfUnAggroRange())
            {
                enemy.ChangeState(EnemyStates.Idle);
                return;
            }

            enemy.TickRepath(deltaTime);
        }

        void IEnemyState.Exit(EnemyBase enemy)
        {
        }
    }
}