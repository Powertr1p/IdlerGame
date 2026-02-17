namespace Enemy.StateMachine
{
    public class DeathState : IEnemyState
    {
        void IEnemyState.Enter(EnemyBase enemy)
        {
            enemy.StopMoving();
            enemy.DisableAgent();
            enemy.PlayDeath();
        }

        void IEnemyState.Tick(EnemyBase enemy, float deltaTime) {}
        void IEnemyState.Exit(EnemyBase enemy) {}
    }
}