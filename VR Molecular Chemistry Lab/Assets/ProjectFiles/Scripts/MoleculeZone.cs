using System.Collections.Generic;
using UnityEngine;

public class MoleculeZone : MonoBehaviour
{
    public BondManager bondManager;

    private List<AtomController> atomsInZone = new List<AtomController>();

    private void OnTriggerEnter(Collider other)
    {
        AtomController atom = other.GetComponent<AtomController>();

        if (atom != null && !atomsInZone.Contains(atom))
        {
            atomsInZone.Add(atom);
            Debug.Log("🟢 Atom Entered: " + atom.atomType);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AtomController atom = other.GetComponent<AtomController>();

        if (atom != null && atomsInZone.Contains(atom))
        {
            atomsInZone.Remove(atom);
            Debug.Log("🔴 Atom Removed: " + atom.atomType);
        }
    }

    // 🔥 CALL THIS WHEN USER RELEASES ATOM (IMPORTANT)
    public void CheckMolecule()
    {
        Debug.Log("🧪 Checking Molecule with count: " + atomsInZone.Count);

        if (bondManager != null)
        {
            bondManager.TryCreateMolecule(new List<AtomController>(atomsInZone));
        }
    }
}