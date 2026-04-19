using System.Collections.Generic;
using UnityEngine;

public class MoleculeZone : MonoBehaviour
{
    public static MoleculeZone Instance;

    public List<AtomController> atomsInZone = new List<AtomController>();

    public bool isProcessing = false;

    private void Awake()
    {
        Instance = this;
    }

    // =========================
    // ADD ATOM
    // =========================
    public void AddAtom(AtomController atom)
    {
        if (atom == null || isProcessing) return;

        if (!atomsInZone.Contains(atom))
        {
            atomsInZone.Add(atom);
            Debug.Log("➕ Added: " + atom.atomType);

            TryFormMolecule();
        }
    }

    // =========================
    // REMOVE ATOM
    // =========================
    public void RemoveAtom(AtomController atom)
    {
        if (atom == null) return;

        if (atomsInZone.Contains(atom))
        {
            atomsInZone.Remove(atom);
            Debug.Log("➖ Removed: " + atom.atomType);
        }
    }

    // =========================
    // DELAYED CHECK (🔥 MAIN FIX)
    // =========================
    void TryFormMolecule()
    {
        if (isProcessing) return;

        // Prevent multiple rapid calls
        CancelInvoke(nameof(ProcessMolecule));

        // 🔥 IMPORTANT DELAY (fixes your issue)
        Invoke(nameof(ProcessMolecule), 0.2f);
    }

    // =========================
    // FINAL PROCESS
    // =========================
    void ProcessMolecule()
    {
        if (isProcessing) return;

        // Clean destroyed atoms
        atomsInZone.RemoveAll(a => a == null);

        if (atomsInZone.Count == 0) return;

        Debug.Log("🧪 FINAL CHECK COUNT: " + atomsInZone.Count);

        BondManager manager = FindAnyObjectByType<BondManager>();

        if (manager != null)
        {
            manager.TryCreateMolecule(new List<AtomController>(atomsInZone));
        }
        else
        {
            Debug.LogError("❌ BondManager NOT FOUND!");
        }
    }

    // =========================
    // CLEAR AFTER SUCCESS
    // =========================
    public void ClearZone()
    {
        atomsInZone.Clear();
    }
}