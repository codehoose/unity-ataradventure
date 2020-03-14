using System;

public delegate void PlayerLeftTheScreenEvent(object sender, PlayerLeftTheScreenEventArgs e);

public class PlayerLeftTheScreenEventArgs : EventArgs
{
    public Direction Direction { get; }

    public PlayerLeftTheScreenEventArgs(Direction direction)
    {
        Direction = direction;
    }
}

