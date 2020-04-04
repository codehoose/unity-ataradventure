using System.Collections.Generic;
using UnityEngine;

public class RoomRenderer : MonoBehaviour
{
    public GameObject blockPrefab;

    public Color[] _roomColours;

    private List<GameObject> _blocks = new List<GameObject>();

    private List<BoxCollider2D> _keyRequired = new List<BoxCollider2D>();

    /// <summary>
    /// When the key is used remove the box colliders from the scene
    /// </summary>
    public void KeyUsed()
    {
        foreach (var collider in _keyRequired)
        {
            Destroy(collider);
        }

        _keyRequired.Clear();
    }

    public void RenderRoom(TextAsset asset, bool gateOpened, int roomColour = 0)
    {
        _keyRequired.Clear();
        _blocks.ForEach(b => Destroy(b));
        _blocks.Clear();

        var offset = new Vector3(19, 10.2f);

        var room = RoomLoader.Load(asset);
        var collisionBlocks = room.GetCollision();
        var visualBlocks = room.GetVisual();
        var keyBlocks = room.GetKeyRequired();
        for (var y = 0; y < 22; y++)
        {
            for (var x = 0; x < 38;x++)
            {
                var collision = collisionBlocks.IsSet(x, y);
                var block = visualBlocks.IsSet(x, y);
                var pos = offset - new Vector3(x, y);

                var keyRequired = keyBlocks != null && 
                                  !gateOpened && 
                                  keyBlocks.IsSet(x, y);

                if (block)
                {
                    var copy = Instantiate(blockPrefab, pos, Quaternion.identity);
                    _blocks.Add(copy);
                    var sprite = copy.GetComponent<SpriteRenderer>();
                    sprite.color = _roomColours[roomColour];
                    if (collision || keyRequired)
                    {
                        var collider = copy.AddComponent<BoxCollider2D>();
                        if (keyRequired)
                        {
                            _keyRequired.Add(collider);
                        }
                    }
                }
            }
        }
    }
}
