using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private BoxCollider2D _collider;
    private PlayerBoundsDetect _playerBoundsDetect;
    private LevelData _currentLevel;
    private int _roomIndex = 0;
    public Transform _player;

    public RoomRenderer _renderer;

    public TextAsset[] _difficulty;

    public TextAsset[] _rooms;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _playerBoundsDetect = new PlayerBoundsDetect(_collider, _player);
        _playerBoundsDetect.PlayerLeftTheScreen += HandlePlayerLeavesPlayfield;
    }

    private void HandlePlayerLeavesPlayfield(object sender, PlayerLeftTheScreenEventArgs e)
    {
        var room = _currentLevel.rooms[_roomIndex];
        if (e.Direction == Direction.North && room.n >= 0)
        {
            ChangeRoom(room.n);
        }
        else if (e.Direction == Direction.South && room.s >= 0)
        {
            ChangeRoom(room.s);
        }
        else if (e.Direction == Direction.West && room.w >= 0)
        {
            ChangeRoom(room.w);
        }
        else if (e.Direction == Direction.East && room.e >= 0)
        {
            ChangeRoom(room.e);
        }
    }

    private void Start()
    {
        _currentLevel = LevelLoader.LoadLevel(_difficulty[0]);
        ChangeRoom(_roomIndex);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _playerBoundsDetect.HandleEvent(collision);
    }

    private void ChangeRoom(int roomIndex)
    {
        _roomIndex = roomIndex;
        var firstRoom = _rooms[_currentLevel.rooms[_roomIndex].shape];
        var colour = _currentLevel.rooms[_roomIndex].colour;

        _renderer.RenderRoom(firstRoom, colour);

    }
}
