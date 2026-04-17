using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AtomDetector : MonoBehaviour
{
    public List<AtomController> nearbyAtoms = new List<AtomController>();
    public BondManager bondManager;

    private AtomController selfAtom;

    private void Start()
    {
        // Get parent atom (IMPORTANT)
        selfAtom = GetComponentInParent<AtomController>();

        if (selfAtom == null)
        {
            Debug.LogError("❌ AtomDetector: No AtomController found in parent!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AtomController atom = other.GetComponent<AtomController>();

        if (atom != null && atom != selfAtom && !nearbyAtoms.Contains(atom))
        {
            nearbyAtoms.Add(atom);
            Debug.Log("🟢 Detected Atom: " + atom.atomType);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AtomController atom = other.GetComponent<AtomController>();

        if (atom != null && nearbyAtoms.Contains(atom))
        {
            nearbyAtoms.Remove(atom);
            Debug.Log("🔴 Removed Atom: " + atom.atomType);
        }
    }

    void Update()
    {
        if (nearbyAtoms.Count >= 1) // + self = minimum 2 atoms
        {
            List<AtomController> allAtoms = new List<AtomController>(nearbyAtoms);

            // Add self atom
            if (selfAtom != null && !allAtoms.Contains(selfAtom))
            {
                allAtoms.Add(selfAtom);
            }

            Debug.Log("🧪 Final Atom List: " + string.Join(", ", allAtoms.Select(a => a.atomType)));

            if (bondManager != null)
            {
                bondManager.TryCreateMolecule(allAtoms);
            }
            else
            {
                Debug.LogError("❌ BondManager is NOT assigned!");
            }
        }
    }
}