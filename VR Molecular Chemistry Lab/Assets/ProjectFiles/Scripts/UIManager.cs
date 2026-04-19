using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hintText;

    public GameObject infoPanel;
    public TextMeshProUGUI infoText;

    public Transform libraryContent;
    public GameObject textPrefab;

    public GameObject successUI;
    public GameObject errorUI;
    public GameObject saveUI;

    private HashSet<string> discovered = new HashSet<string>();
    private Coroutine infoRoutine;

    void Start()
    {
        hintText.gameObject.SetActive(true);
        infoPanel.SetActive(false);

        successUI.SetActive(false);
        errorUI.SetActive(false);
        saveUI.SetActive(false);
    }

    public void ShowMolecule(MoleculeData data)
    {
        if (infoRoutine != null)
            StopCoroutine(infoRoutine);

        hintText.gameObject.SetActive(false);
        infoPanel.SetActive(true);

        infoText.text =
            "Name: " + data.moleculeName + "\n" +
            "Formula: " + data.formula + "\n" +
            "Bond: " + data.bondType;

        ShowSuccess();

        if (!discovered.Contains(data.moleculeName))
        {
            discovered.Add(data.moleculeName);

            GameObject entry = Instantiate(textPrefab, libraryContent);
            entry.GetComponent<TextMeshProUGUI>().text =
                data.moleculeName + " (" + data.formula + ")";

            ShowSave();
        }

        infoRoutine = StartCoroutine(HideInfoAfterDelay(5f));
    }

    IEnumerator HideInfoAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);

        infoPanel.SetActive(false);
        hintText.gameObject.SetActive(true);
    }

    public void ShowError(string msg = "Incomplete Structure")
    {
        errorUI.SetActive(true);

        var txt = errorUI.GetComponentInChildren<TextMeshProUGUI>();
        if (txt != null)
            txt.text = msg;

        StartCoroutine(HideAfter(errorUI, 2f));
    }

    void ShowSuccess()
    {
        successUI.SetActive(true);
        StartCoroutine(HideAfter(successUI, 2f));
    }

    void ShowSave()
    {
        saveUI.SetActive(true);
        StartCoroutine(HideAfter(saveUI, 2f));
    }

    IEnumerator HideAfter(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}