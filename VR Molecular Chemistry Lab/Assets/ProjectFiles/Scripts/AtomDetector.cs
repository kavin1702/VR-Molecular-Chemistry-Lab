using UnityEngine;
using System.Collections.Generic;

public class AtomDetector : MonoBehaviour
{
    public List<AtomController> nearbyAtoms = new List<AtomController>();
    public BondManager bondManager;

    private bool isProcessing = false; void Start()
    {
        if (bondManager == null)
        {
            bondManager = FindObjectOfType<BondManager>();

            if (bondManager == null)
            {
                Debug.LogError("❌ BondManager not found in scene!");
            }
            else
            {
                Debug.Log("✅ BondManager auto assigned");
            }
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        AtomController atom = other.GetComponent<AtomController>();

        if (atom != null && !nearbyAtoms.Contains(atom))
        {
            nearbyAtoms.Add(atom);
            Debug.Log("Atom Entered: " + atom.atomType);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AtomController atom = other.GetComponent<AtomController>();

        if (atom != null && nearbyAtoms.Contains(atom))
        {
            nearbyAtoms.Remove(atom);
            Debug.Log("Atom Exited: " + atom.atomType);
        }
    }

    void Update()
    {
        // 🚨 SAFETY CHECK
        if (bondManager == null)
        {
            Debug.LogError("BondManager NOT assigned!");
            return;
        }

        if (nearbyAtoms.Count >= 2 && !isProcessing)
        {
            isProcessing = true;

            Debug.Log("Trying bond with atoms count: " + nearbyAtoms.Count);

            // 🔥 SNAP EFFECT (soft attraction)
            Vector3 center = GetCenter(nearbyAtoms);

            foreach (var atom in nearbyAtoms)
            {
                atom.transform.position = Vector3.Lerp(
                    atom.transform.position,
                    center,
                    Time.deltaTime * 3f
                );
            }

            // 🔥 TRY CREATE
            bondManager.TryCreateMolecule(new List<AtomController>(nearbyAtoms));

            // Reset flag after small delay
            Invoke(nameof(ResetProcessing), 0.5f);
        }
    }

    void ResetProcessing()
    {
        isProcessing = false;
    }

    Vector3 GetCenter(List<AtomController> atoms)
    {
        Vector3 center = Vector3.zero;

        foreach (var atom in atoms)
        {
            center += atom.transform.position;
        }

        return center / atoms.Count;
    }
}