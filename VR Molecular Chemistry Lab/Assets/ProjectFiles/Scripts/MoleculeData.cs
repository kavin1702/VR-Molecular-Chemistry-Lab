using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Molecule", menuName = "Chemistry/Molecule")]
public class MoleculeData : ScriptableObject
{
    public string moleculeName;
    public string formula;
    public string bondType;

    public List<AtomRequirement> requiredAtoms;

    public GameObject moleculePrefab;
}