using System;
using System.Collections.Generic;
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


    public GameObject _gate;

    public Pickup[] _objects;
    private Dictionary<string, Pickup> _objectMap = new Dictionary<string, Pickup>();

    public event EventHandler RoomChanged;

    public int RoomIndex => _roomIndex;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _playerBoundsDetect = new PlayerBoundsDetect(_collider, _player);
        _playerBoundsDetect.PlayerLeftTheScreen += HandlePlayerLeavesPlayfield;

        foreach (var pickup in _objects)
        {
            _objectMap[pickup.keyName] = pickup;
        }

        _player.GetComponent<Inventory>().ItemDropped += (o, e) =>
          {
              e.Pickup.currentRoom = _roomIndex;
              e.Pickup.position = e.Pickup.transform.position;
          };

        _player.GetComponent<Locomotion>().GateOpened += (o, e) =>
          {
              _currentLevel.rooms[_roomIndex].gate.opened = true;
              _renderer.KeyUsed();
          };
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
        PlaceObjects();
        ChangeRoom(_roomIndex);
    }

    private void PlaceObjects()
    {
        // Go through all the objects in the current level
        var roomId = 0;
        foreach (var room in _currentLevel.rooms)
        {
            if (room.objects == null || room.objects.Length == 0)
            {
                continue;
            }

            foreach (var o in room.objects)
            {
                // Place them in the correct rooms
                _objectMap[o.name].position = new Vector2(o.position.x, o.position.y);
                _objectMap[o.name].currentRoom = roomId;
            }
            roomId++;
        }
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

        ChangePlayerColour(colour);

        _renderer.RenderRoom(firstRoom, _currentLevel.rooms[_roomIndex].gate.opened, colour);

        var gate = _currentLevel.rooms[_roomIndex].gate;
        if (string.IsNullOrEmpty(gate.key) || gate.opened)
        {
            _gate.transform.position = new Vector2(-50, 0);
            _gate.GetComponent<GateActivator>().key = "";
        }
        else
        {
            _gate.GetComponent<GateActivator>().Reset(gate.position, gate.key);
        }

        foreach (var obj in _objects)
        {
            if (obj.currentRoom == roomIndex)
            {
                obj.transform.position = obj.position;
            }
            else if (obj.currentRoom >= 0)
            {
                obj.transform.position = new Vector3(-50, 0);
            }
        }

        RoomChanged?.Invoke(this, EventArgs.Empty);
    }

    private void ChangePlayerColour(int colourIndex)
    {
        var colour = GetComponent<RoomRenderer>()._roomColours[colourIndex];
        var sprite = _player.GetComponent<SpriteRenderer>().color = colour;
    }
}
