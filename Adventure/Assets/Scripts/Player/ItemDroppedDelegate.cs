using System;

public delegate void ItemDroppedEventHandler(object sender, ItemDroppedEventArgs e);

public class ItemDroppedEventArgs : EventArgs
{
    public Pickup Pickup { get; }

    public ItemDroppedEventArgs(Pickup pickup)
    {
        Pickup = pickup;
    }
}
