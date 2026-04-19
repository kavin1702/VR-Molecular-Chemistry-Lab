//using UnityEngine;
//using System.Collections.Generic;

//[CreateAssetMenu(fileName = "New Molecule", menuName = "Chemistry/Molecule")]
//public class MoleculeData : ScriptableObject
//{
//    public string moleculeName;
//    public List<AtomType> requiredAtoms;
//    public GameObject moleculePrefab;
//}

using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Molecule", menuName = "Chemistry/Molecule")]
public class MoleculeData : ScriptableObject
{
    public string moleculeName;
    public string formula;
    public string bondType;

    public List<AtomType> requiredAtoms;
    public GameObject moleculePrefab;
}