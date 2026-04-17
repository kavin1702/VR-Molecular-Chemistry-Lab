using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BondManager : MonoBehaviour
{
    public List<MoleculeData> moleculeDatabase;

    public void TryCreateMolecule(List<AtomController> atoms)
    {
        Debug.Log("🧠 BondManager received atoms: " + atoms.Count);

        List<AtomType> input = atoms.Select(a => a.atomType).ToList();

        Debug.Log("🔍 Input Atoms: " + string.Join(", ", input));

        foreach (var molecule in moleculeDatabase)
        {
            Debug.Log("🔎 Checking against: " + molecule.moleculeName);

            if (MatchAtoms(input, molecule.requiredAtoms))
            {
                Debug.Log("✅ MATCH FOUND: " + molecule.moleculeName);

                CreateMolecule(molecule, atoms);
                return;
            }
        }

        Debug.Log("❌ No matching molecule found");
    }

    bool MatchAtoms(List<AtomType> input, List<AtomType> required)
    {
        if (input.Count != required.Count)
        {
            Debug.Log("⚠️ Count mismatch");
            return false;
        }

        bool isMatch = input.OrderBy(x => x).SequenceEqual(required.OrderBy(x => x));

        Debug.Log("🔄 Matching result: " + isMatch);

        return isMatch;
    }

    void CreateMolecule(MoleculeData molecule, List<AtomController> atoms)
    {
        Debug.Log("🎉 Creating Molecule: " + molecule.moleculeName);

        Vector3 spawnPos = atoms[0].transform.position;

        foreach (var atom in atoms)
        {
            Destroy(atom.gameObject);
        }

        if (molecule.moleculePrefab != null)
        {
            Instantiate(molecule.moleculePrefab, spawnPos, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("⚠️ Molecule prefab is NULL!");
        }
    }
}