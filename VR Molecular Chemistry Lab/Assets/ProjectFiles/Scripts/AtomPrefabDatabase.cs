using UnityEngine;

[System.Serializable]
public class AtomPrefabEntry
{
    public AtomType type;
    public GameObject prefab;
}

public class AtomPrefabDatabase : MonoBehaviour
{
    public AtomPrefabEntry[] atomPrefabs;

    public GameObject GetPrefab(AtomType type)
    {
        foreach (var entry in atomPrefabs)
        {
            if (entry.type == type)
                return entry.prefab;
        }

        Debug.LogError("❌ No prefab found for: " + type);
        return null;
    }
}   