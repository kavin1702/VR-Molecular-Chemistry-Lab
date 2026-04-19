using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Hint")]
    public TextMeshProUGUI hintText;

    [Header("Info Panel")]
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;

    [Header("Library")]
    public Transform libraryContent;
    public GameObject textPrefab;

    [Header("Feedback UI")]
    public GameObject successUI;
    public GameObject errorUI;
    public GameObject saveUI;

    void Start()
    {
        // ✅ Hint visible at start
        if (hintText != null)
            hintText.gameObject.SetActive(true);

        // ✅ Info panel always visible (only text changes)
        if (infoPanel != null)
            infoPanel.SetActive(true);

        // ✅ Clear info text initially
        if (infoText != null)
            infoText.text = "";

        // ✅ Clear old library items (IMPORTANT)
        if (libraryContent != null)
        {
            foreach (Transform child in libraryContent)
            {
                Destroy(child.gameObject);
            }
        }

        // ✅ Hide feedback UI initially
        if (successUI != null) successUI.SetActive(false);
        if (errorUI != null) errorUI.SetActive(false);
        if (saveUI != null) saveUI.SetActive(false);
    }

    // 🟢 SUCCESS MOLECULE
    public void ShowMolecule(MoleculeData data)
    {
        if (data == null) return;

        // ✅ Hide hint only
        if (hintText != null)
            hintText.gameObject.SetActive(false);

        // ✅ Update info text ONLY (panel stays active)
        if (infoText != null)
        {
            infoText.text =
                "Name: " + data.moleculeName + "\n" +
                "Formula: " + data.formula + "\n" +
                "Bond: " + data.bondType;
        }

        // ✅ Add to library (no overlap)
        AddToLibrary(data.moleculeName);

        // ✅ Show success UI
        ShowSuccess();
    }

    // 📚 ADD TO LIBRARY
    void AddToLibrary(string name)
    {
        if (libraryContent == null || textPrefab == null) return;

        GameObject item = Instantiate(textPrefab, libraryContent);

        TextMeshProUGUI txt = item.GetComponent<TextMeshProUGUI>();
        if (txt != null)
        {
            txt.text = name;
        }
    }

    // 🔴 ERROR (AUTO HIDE AFTER 2 SEC)
    public void ShowError(string msg)
    {
        if (errorUI != null)
        {
            errorUI.SetActive(true);
            StartCoroutine(HideErrorAfterDelay());
        }
    }

    IEnumerator HideErrorAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        if (errorUI != null)
            errorUI.SetActive(false);
    }

    // 🟢 SUCCESS UI
    public void ShowSuccess()
    {
        if (successUI != null)
        {
            successUI.SetActive(true);
            StartCoroutine(HideSuccessAfterDelay());
        }
    }

    IEnumerator HideSuccessAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        if (successUI != null)
            successUI.SetActive(false);
    }

    // 💾 SAVE UI (OPTIONAL)
    public void ShowSave()
    {
        if (saveUI != null)
        {
            saveUI.SetActive(true);
            StartCoroutine(HideSaveAfterDelay());
        }
    }

    IEnumerator HideSaveAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        if (saveUI != null)
            saveUI.SetActive(false);
    }
}