using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BondManager : MonoBehaviour
{
    public List<MoleculeData> moleculeDatabase;
    public UIManager uiManager;
    public AudioManager audioManager; // ✅ assign in inspector

    public void TryCreateMolecule(List<AtomController> atoms)
    {
        if (atoms == null || atoms.Count == 0)
        {
            Debug.Log("⚠ No atoms provided");
            return;
        }

        List<AtomType> input = atoms.Select(a => a.atomType).ToList();

        Debug.Log("🧪 Input Atoms: " + string.Join(", ", input));

        foreach (var molecule in moleculeDatabase)
        {
            Debug.Log("🔍 Checking: " + molecule.moleculeName);

            if (MatchAtoms(input, molecule.requiredAtoms))
            {
                Debug.Log("✅ MATCH FOUND: " + molecule.moleculeName);

                CreateMolecule(molecule, atoms);

                if (uiManager != null)
                    uiManager.ShowMolecule(molecule);

                if (audioManager != null)
                    audioManager.PlaySuccess();

                return;
            }
        }

        Debug.Log("❌ No matching molecule found");

        if (uiManager != null)
            uiManager.ShowError("Incomplete Structure");
    }

    // 🔥 STRICT MATCH (NO EXTRA ATOMS)
    bool MatchAtoms(List<AtomType> input, List<AtomRequirement> required)
    {
        Dictionary<AtomType, int> inputCount = new Dictionary<AtomType, int>();

        foreach (var atom in input)
        {
            if (!inputCount.ContainsKey(atom))
                inputCount[atom] = 0;

            inputCount[atom]++;
        }

        // 🔥 total count must match EXACTLY
        int requiredTotal = required.Sum(r => r.count);
        if (input.Count != requiredTotal)
        {
            Debug.Log("❌ Count mismatch");
            return false;
        }

        foreach (var req in required)
        {
            if (!inputCount.ContainsKey(req.atomType))
            {
                Debug.Log("❌ Missing atom: " + req.atomType);
                return false;
            }

            if (inputCount[req.atomType] != req.count)
            {
                Debug.Log("❌ Wrong count for: " + req.atomType);
                return false;
            }
        }

        return true;
    }

    void CreateMolecule(MoleculeData molecule, List<AtomController> atoms)
    {
        Vector3 spawnPos = atoms[0].transform.position;

        Debug.Log("🧬 Creating Molecule: " + molecule.moleculeName);

        foreach (var atom in atoms)
        {
            Destroy(atom.gameObject);
        }

        Instantiate(molecule.moleculePrefab, spawnPos, Quaternion.identity);
    }
}