namespace Enemy.StateMachine
{
    public class EnemyStates
    {
        public static readonly IEnemyState Idle = new IdleState();
        public static readonly IEnemyState Chase = new ChaseState();
        public static readonly IEnemyState Attack = new AttackState();
        public static readonly IEnemyState Death = new DeathState();
    }
}