using UnityEngine;

class RoomLoader
{
    public static RoomData Load(string roomName)
    {
        var textAsset = Resources.Load<TextAsset>(roomName);
        return Load(textAsset);
    }

    public static RoomData Load(TextAsset textAsset)
    {
        var roomData = JsonUtility.FromJson<RoomData>(textAsset.text);
        return roomData;
    }
}