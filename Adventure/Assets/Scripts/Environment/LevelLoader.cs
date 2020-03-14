using UnityEngine;

static class LevelLoader
{
    public static LevelData LoadLevel(TextAsset levelData)
    {
        var json = levelData.text;
        return JsonUtility.FromJson<LevelData>(json);
    }
}