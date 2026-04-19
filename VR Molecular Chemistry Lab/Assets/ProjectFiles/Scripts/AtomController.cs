using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class AtomController : MonoBehaviour
{
    public AtomType atomType;

    private XRGrabInteractable grab;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    void Start()
    {
        Renderer r = GetComponent<Renderer>();

        switch (atomType)
        {
            case AtomType.Hydrogen:
                r.material.color = Color.white;
                break;

            case AtomType.Oxygen:
                r.material.color = Color.red;
                break;

            case AtomType.Carbon:
                r.material.color = Color.black;
                break;

            case AtomType.Nitrogen:
                r.material.color = Color.blue;
                break;
        }
    }   

    private void OnEnable()
    {
        grab.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grab.selectExited.RemoveListener(OnRelease);
    }

    void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log("Released: " + atomType);

        // 🔥 Tell zone to check
        MoleculeZone.Instance.CheckMolecule();
    }
}