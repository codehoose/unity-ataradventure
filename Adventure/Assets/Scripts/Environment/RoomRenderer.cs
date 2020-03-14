using System.Collections.Generic;
using UnityEngine;

public class RoomRenderer : MonoBehaviour
{
    public GameObject blockPrefab;

    public Color[] _roomColours;

    private List<GameObject> _blocks = new List<GameObject>();

    public void RenderRoom(TextAsset asset, int roomColour = 0)
    {
        _blocks.ForEach(b => Destroy(b));
        _blocks.Clear();

        var offset = new Vector3(19, 10.2f);

        var room = RoomLoader.Load(asset);
        for (var y = 0; y < 22; y++)
        {
            for (var x = 0; x < 38;x++)
            {
                var collision = room.GetCollision().IsSet(x, y);
                var block = room.GetVisual().IsSet(x, y);
                var pos = offset - new Vector3(x, y);

                if (block)
                {
                    var copy = Instantiate(blockPrefab, pos, Quaternion.identity);
                    _blocks.Add(copy);
                    var sprite = copy.GetComponent<SpriteRenderer>();
                    sprite.color = _roomColours[roomColour];
                    if (collision)
                    {
                        copy.AddComponent<BoxCollider2D>();
                    }
                }
            }
        }
    }
}
