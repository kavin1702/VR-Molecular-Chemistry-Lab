using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI infoText;

    public void ShowMolecule(string name)
    {
        infoText.text = "Formed: " + name;
    }
}