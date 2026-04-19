using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MoleculeController : MonoBehaviour
{
    public MoleculeData data;
    public GameObject atomPrefab;

    private XRGrabInteractable grab;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
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
        Debug.Log("Breaking molecule: " + data.moleculeName);

        BreakMolecule();
    }

    void BreakMolecule()
    {
        Vector3 center = transform.position;

        foreach (var atomType in data.requiredAtoms)
        {
            Vector3 offset = Random.insideUnitSphere * 0.3f;

            GameObject atom = Instantiate(atomPrefab, center + offset, Quaternion.identity);

            atom.GetComponent<AtomController>().atomType = atomType;
        }

        Destroy(gameObject);
    }
}