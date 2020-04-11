using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private AudioController _audio;
    private AIController _ai;
    private BoxCollider2D _collider;
    private PlayerBoundsDetect _playerBoundsDetect;
    private LevelData _currentLevel;
    private int _roomIndex = 0;
    private string _currentItem;
    public Transform _player;

    public RoomRenderer _renderer;

    public TextAsset[] _difficulty;

    public TextAsset[] _rooms;


    public GameObject _gate;

    public Pickup[] _objects;
    private Dictionary<string, Pickup> _objectMap = new Dictionary<string, Pickup>();

    public event EventHandler RoomChanged;

    public event PlayfieldBoundsEventHandler PlayerLeftScreen;

    public int RoomIndex => _roomIndex;

    public string CurremtItem => _currentItem;

    public void GameOver(bool wasKilledByDragon = false)
    {
        if (wasKilledByDragon)
        {
            _audio.PlayPlayerDead();
        }
    }

    private void Awake()
    {
        _ai = GetComponent<AIController>();
        _audio = GetComponent<AudioController>();
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
              _currentItem = "";
              _audio.PlayDrop();
          };

        _player.GetComponent<Inventory>().ItemPickedUp += (o, e) =>
          {
              _currentItem = e.Pickup.keyName;
              _audio.PlayPickup();
          };

        _player.GetComponent<Locomotion>().GateOpened += (o, e) =>
          {
              _currentLevel.rooms[_roomIndex].gate.opened = true;
              _renderer.KeyUsed();
          };

        _player.GetComponent<Locomotion>().DragonBite += (o, e) =>
          {
              _audio.PlayDragonBite();
          };
    }

    internal void DragonHit()
    {
        _audio.PlaySwordHit();
    }

    public void DragonDead()
    {
        _audio.PlayDragonDead();
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

        PlayerLeftScreen?.Invoke(this, new PlayfieldBoundsEventArgs(e.Direction));
    }

    private void Start()
    {
        _currentLevel = LevelLoader.LoadLevel(_difficulty[0]);
        PlaceObjects();
        _ai.PlaceDragons(_currentLevel.rooms);
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
                roomId++;
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
        _player.GetComponent<SpriteRenderer>().color = colour;
    }
}
