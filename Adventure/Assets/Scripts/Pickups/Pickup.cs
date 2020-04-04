using UnityEngine;

public class Pickup : MonoBehaviour
{
    [HideInInspector]
    public Vector2 position;

    public string keyName;

    [HideInInspector]
    public int currentRoom = -1;
}
