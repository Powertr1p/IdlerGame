namespace Enemy.StateMachine
{
    public interface IEnemyState
    {
        public void Enter(EnemyBase enemy);
        public void Tick(EnemyBase enemy, float deltaTime);
        public void Exit(EnemyBase enemy);
    }
}