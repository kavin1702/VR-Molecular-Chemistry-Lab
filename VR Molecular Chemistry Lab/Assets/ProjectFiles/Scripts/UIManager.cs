//using UnityEngine;
//using TMPro;
//using System.Collections;
//using System.Collections.Generic;

//public class UIManager : MonoBehaviour
//{
//    public GameObject infoPanel;
//    public TextMeshProUGUI infoText;

//    public Transform libraryContent;
//    public GameObject textPrefab;

//    private HashSet<string> discovered = new HashSet<string>();

//    public void ShowMolecule(MoleculeData data)
//    {
//        // 🟣 INFO PANEL
//        infoPanel.SetActive(true);

//        infoText.text =
//            "Name: " + data.moleculeName + "\n" +
//            "Formula: " + data.formula + "\n" +
//            "Bond: " + data.bondType;

//        StopAllCoroutines();
//        StartCoroutine(HideInfo());

//        // 🟣 LIBRARY PANEL
//        if (!discovered.Contains(data.moleculeName))
//        {
//            discovered.Add(data.moleculeName);

//            GameObject entry = Instantiate(textPrefab, libraryContent);
//            entry.GetComponent<TextMeshProUGUI>().text =
//                data.moleculeName + " (" + data.formula + ")";
//        }
//    }

//    IEnumerator HideInfo()
//    {
//        yield return new WaitForSeconds(3f);
//        infoPanel.SetActive(false);
//    }
//}

using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [Header("INFO PANEL")]
    public TextMeshProUGUI infoText;

    [Header("LIBRARY")]
    public Transform libraryContent;
    public GameObject textPrefab;

    [Header("FEEDBACK UI")]
    public GameObject successUI;
    public GameObject errorUI;
    public GameObject saveUI;

    private HashSet<string> discovered = new HashSet<string>();

    // 🟢 CALLED WHEN MOLECULE CREATED
    public void ShowMolecule(MoleculeData data)
    {
        // 🟦 INFO PANEL TEXT
        infoText.text =
            "Name: " + data.moleculeName + "\n" +
            "Formula: " + data.formula + "\n" +
            "Bond: " + data.bondType;

        StopAllCoroutines();
        StartCoroutine(HideInfo());

        // 🟩 SUCCESS UI
        ShowSuccess();

        // 📚 LIBRARY ADD (ONLY ONCE)
        if (!discovered.Contains(data.moleculeName))
        {
            discovered.Add(data.moleculeName);

            GameObject entry = Instantiate(textPrefab, libraryContent);
            entry.GetComponent<TextMeshProUGUI>().text =
                data.moleculeName + " (" + data.formula + ")";

            // 💾 SAVE UI (FIRST TIME ONLY)
            ShowSave();
        }
    }

    // ❌ ERROR UI
    public void ShowError(string message = "Incomplete Structure")
    {
        if (errorUI != null)
        {
            errorUI.SetActive(true);

            TextMeshProUGUI txt = errorUI.GetComponentInChildren<TextMeshProUGUI>();
            if (txt != null)
                txt.text = message;

            StartCoroutine(HideAfter(errorUI, 2f));
        }
    }

    // ✅ SUCCESS UI
    void ShowSuccess()
    {
        if (successUI != null)
        {
            successUI.SetActive(true);
            StartCoroutine(HideAfter(successUI, 2f));
        }
    }

    // 💾 SAVE UI
    void ShowSave()
    {
        if (saveUI != null)
        {
            saveUI.SetActive(true);
            StartCoroutine(HideAfter(saveUI, 2f));
        }
    }

    // ⏳ CLEAR ONLY TEXT (NOT PANEL)
    IEnumerator HideInfo()
    {
        yield return new WaitForSeconds(3f);
        infoText.text = "";
    }

    // ⏳ GENERIC HIDE FUNCTION
    IEnumerator HideAfter(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}