using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BondManager : MonoBehaviour
{
    public List<MoleculeData> moleculeDatabase;
    public AudioManager audioManager;
    public UIManager uiManager;

    public void TryCreateMolecule(List<AtomController> atoms)
    {
        List<AtomType> input = atoms.Select(a => a.atomType).ToList();

        foreach (var molecule in moleculeDatabase)
        {
            if (MatchAtoms(input, molecule.requiredAtoms))
            {
                CreateMolecule(molecule, atoms);
                return;
            }
        }
    }

    bool MatchAtoms(List<AtomType> input, List<AtomType> required)
    {
        if (input.Count != required.Count) return false;
        return input.OrderBy(x => x).SequenceEqual(required.OrderBy(x => x));
    }

    void CreateMolecule(MoleculeData molecule, List<AtomController> atoms)
    {
        Vector3 pos = atoms[0].transform.position;

        foreach (var atom in atoms)
        {
            Destroy(atom.gameObject);
        }

        GameObject mol = Instantiate(molecule.moleculePrefab, pos, Quaternion.identity);

        audioManager.PlaySuccess();
        uiManager.ShowMolecule(molecule.moleculeName);
    }
}