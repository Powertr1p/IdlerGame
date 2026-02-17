namespace Enemy.StateMachine
{
    public class AttackState : IEnemyState
    {
        void IEnemyState.Enter(EnemyBase enemy)
        {
            enemy.StopMoving();
            enemy.PlayAttack();
            enemy.ResetAttackTimer();
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

            if (!enemy.IsInAttackRange())
            {
                enemy.ChangeState(enemy.IsInAggroRange() ? EnemyStates.Chase : EnemyStates.Idle);

                return;
            }

            enemy.TickAttack(deltaTime);
        }

        void IEnemyState.Exit(EnemyBase enemy)
        {
        }
    }
}