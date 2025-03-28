
public class PlayerStateMachine : BaseStateMachine<PlayerController>
{
    public PlayerNormalState normalState;
    public PlayerCarryState carryState;

    public PlayerStateMachine(PlayerController player)
    {
        this.normalState = new PlayerNormalState(player);
        this.carryState = new PlayerCarryState(player);
    }
}
