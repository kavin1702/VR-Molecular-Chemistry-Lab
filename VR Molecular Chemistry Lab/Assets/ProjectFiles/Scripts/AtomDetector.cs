using UnityEngine;
using System.Collections.Generic;

public class AtomDetector : MonoBehaviour
{
    public List<AtomController> nearbyAtoms = new List<AtomController>();
    public BondManager bondManager;

    private AtomController selfAtom;

    void Start()
    {
        selfAtom = GetComponentInParent<AtomController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        AtomController atom = other.GetComponent<AtomController>();

        if (atom != null && atom != selfAtom && !nearbyAtoms.Contains(atom))
        {
            nearbyAtoms.Add(atom);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AtomController atom = other.GetComponent<AtomController>();

        if (atom != null && nearbyAtoms.Contains(atom))
        {
            nearbyAtoms.Remove(atom);
        }
    }

    void Update()
    {
        if (nearbyAtoms.Count > 0)
        {
            List<AtomController> allAtoms = new List<AtomController>(nearbyAtoms);
            allAtoms.Add(selfAtom);

            bondManager.TryCreateMolecule(allAtoms);
        }
    }
}