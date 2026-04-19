using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HighlightController : MonoBehaviour
{
    public Renderer targetRenderer;

    public Color normalColor = Color.white;
    public Color hoverColor = Color.yellow;
    public Color nearColor = Color.cyan;

    private XRGrabInteractable grab;
    private Material mat;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        mat = targetRenderer.material;

        grab.hoverEntered.AddListener(OnHoverEnter);
        grab.hoverExited.AddListener(OnHoverExit);
    }

    void OnHoverEnter(HoverEnterEventArgs args)
    {
        SetColor(hoverColor);
    }

    void OnHoverExit(HoverExitEventArgs args)
    {
        SetColor(normalColor);
    }

    public void SetNear(bool isNear)
    {
        SetColor(isNear ? nearColor : normalColor);
    }

    void SetColor(Color c)
    {
        mat.color = c;
    }
}