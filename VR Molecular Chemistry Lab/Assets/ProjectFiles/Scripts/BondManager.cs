using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BondManager : MonoBehaviour
{
    public List<MoleculeData> moleculeDatabase;
    public UIManager uiManager;
    public AudioManager audioManager;

    public void TryCreateMolecule(List<AtomController> atoms)
    {
        if (MoleculeZone.Instance.isProcessing) return;

        atoms = atoms.Where(a => a != null).ToList();
        if (atoms.Count == 0) return;

        List<AtomType> input = atoms.Select(a => a.atomType).ToList();

        Debug.Log("🧪 Input: " + string.Join(", ", input));

        foreach (var molecule in moleculeDatabase)
        {
            if (MatchAtoms(input, molecule.requiredAtoms))
            {
                Debug.Log("✅ MATCH FOUND: " + molecule.moleculeName);

                MoleculeZone.Instance.isProcessing = true;

                CreateMolecule(molecule, atoms);

                uiManager?.ShowMolecule(molecule);
                audioManager?.PlaySuccess();

                Invoke(nameof(UnlockZone), 0.5f);
                return;
            }
        }

        uiManager?.ShowError("Invalid Structure");
    }

    void UnlockZone()
    {
        MoleculeZone.Instance.ClearZone();
        MoleculeZone.Instance.isProcessing = false;
    }

    bool MatchAtoms(List<AtomType> input, List<AtomRequirement> required)
    {
        Dictionary<AtomType, int> count = new Dictionary<AtomType, int>();

        foreach (var atom in input)
        {
            if (!count.ContainsKey(atom))
                count[atom] = 0;

            count[atom]++;
        }

        foreach (var req in required)
        {
            if (!count.ContainsKey(req.atomType)) return false;
            if (count[req.atomType] != req.count) return false;
        }

        return true;
    }

    void CreateMolecule(MoleculeData molecule, List<AtomController> atoms)
    {
        Vector3 pos = atoms[0].transform.position;

        foreach (var atom in atoms)
        {
            if (atom != null)
                Destroy(atom.gameObject);
        }

        Instantiate(molecule.moleculePrefab, pos, Quaternion.identity);
    }
}