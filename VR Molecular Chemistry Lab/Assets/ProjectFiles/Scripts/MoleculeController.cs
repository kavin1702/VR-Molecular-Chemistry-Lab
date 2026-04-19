using UnityEngine;

public class MoleculeController : MonoBehaviour
{
    public MoleculeData data;

    private AtomPrefabDatabase atomDatabase;
    private bool isBroken = false;

    private void Awake()
    {
        atomDatabase = FindAnyObjectByType<AtomPrefabDatabase>();
    }

    // 🔥 RECEIVE BREAK POSITION
    public void BreakFromZone(Vector3 breakPosition)
    {
        if (isBroken) return;

        if (atomDatabase == null || data == null)
        {
            Debug.LogError("❌ Missing Database or Data!");
            return;
        }

        isBroken = true;

        foreach (var req in data.requiredAtoms)
        {
            for (int i = 0; i < req.count; i++)
            {
                GameObject prefab = atomDatabase.GetPrefab(req.atomType);
                if (prefab == null) continue;

                Vector3 offset = Random.insideUnitSphere * 0.5f;

                GameObject atom = Instantiate(
                    prefab,
                    breakPosition + offset,   // ✅ FIXED HERE
                    Quaternion.identity
                );

                atom.GetComponent<AtomController>().atomType = req.atomType;
            }
        }

        Destroy(gameObject);
    }
}