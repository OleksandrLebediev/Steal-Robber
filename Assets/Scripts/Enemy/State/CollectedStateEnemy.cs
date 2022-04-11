public class CollectedStateEnemy : BaseState
{
    public CollectedStateEnemy(IStationStateSwitcher stateSwitcher, Enemy enemy) : base(stateSwitcher)
    {
        _enemy = enemy;
    }

    private Enemy _enemy;

    public override void Enter()
    {
        _enemy.Animator.SetBool(EnemyAnimationInfo.Falling, true);
        _enemy.EnemyVision.Hide();
    }

    public override void Exit()
    {
        _enemy.Animator.SetBool(EnemyAnimationInfo.Falling, false);
    }

}
