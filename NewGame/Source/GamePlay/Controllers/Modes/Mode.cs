public abstract class Mode
{
    protected readonly Player player;

    protected readonly float horizontalAcceleration;
    protected readonly float horizontalDeceleration;
    protected readonly float maxSpeed;

    protected readonly float gravity;
    protected readonly float maxFallSpeed;

    public Mode(Player PLAYER, float HORIZONTAL_ACC, float HORIZONTAL_DEC, float MAXSPEED, float GRAVITY, float MAXFALLSPEED)
    {
        player = PLAYER;
        horizontalAcceleration = HORIZONTAL_ACC;
        horizontalDeceleration = HORIZONTAL_DEC;
        maxSpeed = MAXSPEED;
        gravity = GRAVITY;
        maxFallSpeed = MAXFALLSPEED;
    }

    public abstract void MovementControl();
}