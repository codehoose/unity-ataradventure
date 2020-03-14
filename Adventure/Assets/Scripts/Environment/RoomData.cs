
using System;
using System.Linq;

[Serializable]
class RoomData
{
    public LayerData[] layers;

    public LayerData GetVisual()
    {
        return layers.First((d) => d.name == "visual");
    }

    public LayerData GetCollision()
    {
        return layers.First((d) => d.name == "collision");
    }
}

