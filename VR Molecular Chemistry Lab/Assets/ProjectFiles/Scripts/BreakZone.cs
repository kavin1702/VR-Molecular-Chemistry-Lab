using UnityEngine;

public class BreakZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Molecule"))
        {
            MoleculeController molecule = other.GetComponent<MoleculeController>();

            if (molecule != null)
            {
                Debug.Log("💥 Breaking at BreakZone");

                // 🔥 Pass BreakZone position
                molecule.BreakFromZone(transform.position);
            }
        }
    }
}