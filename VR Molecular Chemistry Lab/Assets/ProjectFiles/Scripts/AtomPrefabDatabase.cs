using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AtomPrefabEntry
{
    public AtomType type;
    public GameObject prefab;
}

public class AtomPrefabDatabase : MonoBehaviour
{
    public List<AtomPrefabEntry> atomPrefabs;

    public GameObject GetPrefab(AtomType type)
    {
        foreach (var item in atomPrefabs)
        {
            if (item.type == type)
                return item.prefab;
        }

        Debug.LogError("❌ Prefab not found: " + type);
        return null;
    }
}