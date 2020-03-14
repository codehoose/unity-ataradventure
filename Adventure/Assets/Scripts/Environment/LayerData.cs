using System;

[Serializable]
public class LayerData
{
    public int[] data;
    public string name;
    public int height;
    public int width;
    public int x;
    public int y;

    public bool IsSet(int x, int y)
    {
        var index = y * 38 + x;
        return data[index] != 0;
    }
}
