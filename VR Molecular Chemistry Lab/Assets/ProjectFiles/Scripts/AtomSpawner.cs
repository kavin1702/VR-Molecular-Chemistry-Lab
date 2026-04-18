using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class AtomSpawner : MonoBehaviour
{
    public GameObject atomPrefab;

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        GameObject atom = Instantiate(atomPrefab, transform.position, Quaternion.identity);

        var grab = atom.GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener((args) => Spawn());
    }
}