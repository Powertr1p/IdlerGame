namespace Enemy.StateMachine
{
    public sealed class IdleState : IEnemyState
    {
        void IEnemyState.Enter(EnemyBase enemy)
        {
            enemy.StartIdle();
        }

        void IEnemyState.Tick(EnemyBase enemy, float deltaTime)
        {
            if (!enemy.IsAlive) return;
            if (!enemy.HasTarget) return;

            if (enemy.IsInAttackRange())
            {
                enemy.ChangeState(EnemyStates.Attack);
                return;
            }

            if (enemy.IsInAggroRange())
            {
                enemy.ChangeState(EnemyStates.Chase);
                return;
            }
        }

        void IEnemyState.Exit(EnemyBase enemy)
        {
        }
    }
}