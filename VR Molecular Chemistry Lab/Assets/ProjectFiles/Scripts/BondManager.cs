//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;

//public class BondManager : MonoBehaviour
//{
//    public List<MoleculeData> moleculeDatabase;
//    public UIManager uiManager;

//    public void TryCreateMolecule(List<AtomController> atoms)
//    {
//        List<AtomType> input = atoms.Select(a => a.atomType).ToList();

//        Debug.Log("Input Atoms: " + string.Join(", ", input));

//        foreach (var molecule in moleculeDatabase)
//        {
//            Debug.Log("Checking against: " + molecule.moleculeName);

//            if (MatchAtoms(input, molecule.requiredAtoms))
//            {
//                Debug.Log("MATCH FOUND: " + molecule.moleculeName);

//                CreateMolecule(molecule, atoms);

//                // 🔥 UI TRIGGER
//                if (uiManager != null)
//                {
//                    uiManager.ShowMolecule(molecule);
//                }

//                return;
//            }
//        }

//        Debug.Log("No matching molecule found");
//    }

//    bool MatchAtoms(List<AtomType> input, List<AtomType> required)
//    {
//        if (input.Count != required.Count) return false;

//        return input.OrderBy(x => x).SequenceEqual(required.OrderBy(x => x));
//    }

//    void CreateMolecule(MoleculeData molecule, List<AtomController> atoms)
//    {
//        Vector3 spawnPos = atoms[0].transform.position;

//        foreach (var atom in atoms)
//        {
//            Destroy(atom.gameObject);
//        }

//        Instantiate(molecule.moleculePrefab, spawnPos, Quaternion.identity);
//    }
//}


using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BondManager : MonoBehaviour
{
    public List<MoleculeData> moleculeDatabase;
    public UIManager uiManager;

    public void TryCreateMolecule(List<AtomController> atoms)
    {
        if (atoms == null || atoms.Count == 0)
            return;

        List<AtomType> input = atoms.Select(a => a.atomType).ToList();

        Debug.Log("Input Atoms: " + string.Join(", ", input));

        foreach (var molecule in moleculeDatabase)
        {
            Debug.Log("Checking against: " + molecule.moleculeName);

            if (MatchAtoms(input, molecule.requiredAtoms))
            {
                Debug.Log("✅ MATCH FOUND: " + molecule.moleculeName);

                CreateMolecule(molecule, atoms);

                // 🟢 UI SUCCESS
                if (uiManager != null)
                {
                    uiManager.ShowMolecule(molecule);
                }

                return;
            }
        }

        // ❌ NO MATCH
        Debug.Log("❌ No matching molecule found");

        if (uiManager != null)
        {
            uiManager.ShowError("Incomplete Structure");
        }
    }

    bool MatchAtoms(List<AtomType> input, List<AtomType> required)
    {
        if (input.Count != required.Count)
            return false;

        return input.OrderBy(x => x).SequenceEqual(required.OrderBy(x => x));
    }

    void CreateMolecule(MoleculeData molecule, List<AtomController> atoms)
    {
        Vector3 spawnPos = atoms[0].transform.position;

        // Destroy atoms
        foreach (var atom in atoms)
        {
            Destroy(atom.gameObject);
        }

        // Spawn molecule prefab
        Instantiate(molecule.moleculePrefab, spawnPos, Quaternion.identity);
    }
}