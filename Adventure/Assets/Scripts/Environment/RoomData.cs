
using System;
using System.Linq;

[Serializable]
class RoomData
{
    public LayerData[] layers;

    public LayerData GetVisual()
    {
        return layers.FirstOrDefault((d) => d.name == "visual");
    }

    public LayerData GetCollision()
    {
        return layers.FirstOrDefault((d) => d.name == "collision");
    }

    public LayerData GetKeyRequired()
    {
        return layers.FirstOrDefault((d) => d.name == "key-required");
    }
}

