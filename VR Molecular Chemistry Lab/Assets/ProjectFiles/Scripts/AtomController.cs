using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class AtomController : MonoBehaviour
{
    public AtomType atomType;

    private XRGrabInteractable grab;
    private Rigidbody rb;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (grab != null)
        {
            grab.selectEntered.AddListener(OnGrabbed);
            grab.selectExited.AddListener(OnReleased);
        }
    }

    private void OnDisable()
    {
        if (grab != null)
        {
            grab.selectEntered.RemoveListener(OnGrabbed);
            grab.selectExited.RemoveListener(OnReleased);
        }
    }

    // 🟢 GRAB EVENT
    void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("🤏 Grabbed: " + atomType);

        // ✅ Ensure physics works while holding
        if (rb != null)
            rb.isKinematic = false;

        // ✅ Remove from zone immediately (VERY IMPORTANT)
        if (MoleculeZone.Instance != null)
            MoleculeZone.Instance.RemoveAtom(this);

        // 🔊 PLAY GRAB SFX (FINAL FIX)
        AudioManager am = FindAnyObjectByType<AudioManager>();
        if (am != null)
        {
            am.PlayGrab();
        }
        else
        {
            Debug.LogWarning("⚠️ AudioManager NOT found!");
        }
    }

    // 🔵 RELEASE EVENT
    void OnReleased(SelectExitEventArgs args)
    {
        Debug.Log("🖐 Released: " + atomType);
        // XR Toolkit handles physics automatically
    }

    // 🟡 ENTER ZONE
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MoleculeZone"))
        {
            MoleculeZone zone = other.GetComponent<MoleculeZone>();
            if (zone != null)
            {
                Debug.Log("🟢 Enter Zone: " + atomType);
                zone.AddAtom(this);
            }
        }
    }

    // 🔴 EXIT ZONE
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MoleculeZone"))
        {
            MoleculeZone zone = other.GetComponent<MoleculeZone>();
            if (zone != null)
            {
                Debug.Log("🔴 Exit Zone: " + atomType);
                zone.RemoveAtom(this);
            }
        }
    }

    // 🧹 CLEANUP
    private void OnDestroy()
    {
        if (MoleculeZone.Instance != null)
        {
            MoleculeZone.Instance.RemoveAtom(this);
        }
    }
}