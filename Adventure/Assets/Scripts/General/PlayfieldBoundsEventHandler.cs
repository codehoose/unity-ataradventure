using System;

public delegate void PlayfieldBoundsEventHandler(object sender, PlayfieldBoundsEventArgs e);

public class PlayfieldBoundsEventArgs : EventArgs
{
    public Direction Direction { get; }

    public PlayfieldBoundsEventArgs(Direction direction)
    {
        Direction = direction;
    }
}

