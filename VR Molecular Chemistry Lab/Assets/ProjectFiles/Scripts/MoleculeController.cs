using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MoleculeController : MonoBehaviour
{
    public MoleculeData data;

    private AtomPrefabDatabase atomDatabase;
    private XRGrabInteractable grab;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();

        // ✅ AUTO FIND DATABASE (IMPORTANT FIX)
        atomDatabase = FindObjectOfType<AtomPrefabDatabase>();

        if (atomDatabase == null)
        {
            Debug.LogError("❌ AtomDatabase NOT FOUND in scene!");
        }
    }

    private void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrab);
    }

    private void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrab);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("🧬 Breaking: " + data.moleculeName);
        BreakMolecule();
    }

    void BreakMolecule()
    {
        Vector3 center = transform.position;

        foreach (var req in data.requiredAtoms)
        {
            Debug.Log("Spawning: " + req.atomType + " x" + req.count);

            for (int i = 0; i < req.count; i++)
            {
                GameObject prefab = atomDatabase.GetPrefab(req.atomType);

                if (prefab == null)
                {
                    Debug.LogError("❌ Missing prefab for: " + req.atomType);
                    continue;
                }

                Vector3 offset = Random.insideUnitSphere * 0.3f;

                GameObject atom = Instantiate(prefab, center + offset, Quaternion.identity);

                // ✅ SAFE SET TYPE
                AtomController controller = atom.GetComponent<AtomController>();
                if (controller != null)
                {
                    controller.atomType = req.atomType;
                }
            }
        }

        Destroy(gameObject);
    }
}