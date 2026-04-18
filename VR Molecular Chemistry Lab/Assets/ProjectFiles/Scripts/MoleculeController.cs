using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MoleculeController : MonoBehaviour
{
    public List<GameObject> atomPrefabs;

    void Start()
    {
        var grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        BreakMolecule();
    }

    void BreakMolecule()
    {
        Vector3 pos = transform.position;

        foreach (var atom in atomPrefabs)
        {
            Instantiate(atom, pos + Random.insideUnitSphere * 0.2f, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}