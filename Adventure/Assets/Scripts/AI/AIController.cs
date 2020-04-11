using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Dragon[] _dragons;
    private Dictionary<string, DragonPlacement> _map =
            new Dictionary<string, DragonPlacement>();

    void Awake()
    {
        // A bit hacky, but we know that AIController sits on the
        // same game objects as GameController
        GetComponent<GameController>().RoomChanged += Room_Changed;

        foreach (var dragon in _dragons)
        {
            _map[dragon._name] = new DragonPlacement
            {
                _dragon = dragon,
                _initialRoom = -1,
                _position = Vector2.zero
            };
        }
    }

    private void Room_Changed(object sender, EventArgs e)
    {
        var controller = sender as GameController;
        var roomId = controller.RoomIndex;
        // Get the dragons that are in the current room
        var dragons = _map.Values.Where((placement) =>
                       placement._initialRoom == roomId).ToList();
        // Set them to be active
        foreach (var placement in dragons)
        {
            placement._dragon.transform.position = placement._position;
            placement._dragon.StartChase(controller, placement._initialRoom);
            // Set the index of the current room to be -1
            placement._initialRoom = -1; // Don't reset the position of the dragon
        }
    }

    internal void PlaceDragons(MapRoomData[] rooms)
    {
        var roomIndex = 0;
        foreach (var room in rooms)
        {
            if (room.dragons != null && room.dragons.Length > 0)
            {
                foreach (var dragon in room.dragons)
                {
                    _map[dragon.name]._initialRoom = roomIndex;
                    _map[dragon.name]._position = dragon.position;
                }
            }

            roomIndex++;
        }
    }
}
