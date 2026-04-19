using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class AtomController : MonoBehaviour
{
    public AtomType atomType;

    private XRGrabInteractable grab;
    private MoleculeZone currentZone;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrab);
        grab.selectExited.RemoveListener(OnRelease);
    }

    // ✅ THIS FIXES YOUR ERROR
    public void SetAtomType(AtomType type)
    {
        atomType = type;
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("🟡 Grabbed: " + atomType);

        AudioManager audio = FindObjectOfType<AudioManager>();
        if (audio != null)
            audio.PlayGrab();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MoleculeZone"))
        {
            currentZone = other.GetComponent<MoleculeZone>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MoleculeZone"))
        {
            currentZone = null;
        }
    }

    void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log("🖐 Released: " + atomType);

        if (currentZone != null)
        {
            currentZone.CheckMolecule();
        }
    }
}