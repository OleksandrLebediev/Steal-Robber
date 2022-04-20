using UnityEngine;
public class ChoicePatrolStateEnemy : BaseState
{
    public ChoicePatrolStateEnemy(IStationStateSwitcher stateSwitcher, EnemyMovement movement) : base(stateSwitcher)
    {
        _movement = movement;
    }

    private EnemyMovement _movement;

    public override void Enter()
    {
        switch (_movement.Patrole)
        {
            case PatroleType.Turn:
                _stateSwitcher.SwitchState<PatrolTurnStateEnemy>();
                break;
            case PatroleType.Stand:
                _stateSwitcher.SwitchState<PatrolStandStateEnemy>();
                break;
            case PatroleType.Loop:
                _stateSwitcher.SwitchState<PatrolLoopStateEnemy>();
                break;
            default:
                break;
        }

    }
}
public enum PatroleType
{
    Turn,
    Stand,
    Loop
}


