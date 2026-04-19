using UnityEngine;
using System.Collections.Generic;

public class MoleculeZone : MonoBehaviour
{
    public static MoleculeZone Instance;

    public List<AtomController> atomsInZone = new List<AtomController>();

    public BondManager bondManager;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        AtomController atom = other.GetComponent<AtomController>();

        if (atom != null && !atomsInZone.Contains(atom))
        {
            atomsInZone.Add(atom);
            Debug.Log("Added: " + atom.atomType);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AtomController atom = other.GetComponent<AtomController>();

        if (atom != null && atomsInZone.Contains(atom))
        {
            atomsInZone.Remove(atom);
            Debug.Log("Removed: " + atom.atomType);
        }
    }

    public void CheckMolecule()
    {
        if (bondManager == null)
        {
            Debug.LogError("BondManager missing!");
            return;
        }

        if (atomsInZone.Count < 2)
        {
            Debug.Log("Not enough atoms");
            return;
        }

        Debug.Log("Checking molecule with count: " + atomsInZone.Count);

        bondManager.TryCreateMolecule(new List<AtomController>(atomsInZone));
    }
}