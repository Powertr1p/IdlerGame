namespace Enemy.StateMachine
{
    public class DeathState : IEnemyState
    {
        void IEnemyState.Enter(EnemyBase enemy)
        {
            enemy.StartDeath();
        }

        void IEnemyState.Tick(EnemyBase enemy, float deltaTime) {}
        void IEnemyState.Exit(EnemyBase enemy) {}
    }
}