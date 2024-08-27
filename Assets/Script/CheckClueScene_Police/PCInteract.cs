using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PCInteract : MonoBehaviour
{
    public GameObject changeBG;
    public GameObject findEvidenceManager;

    [SerializeField] TMP_InputField inputField_password;
    [SerializeField] GameObject passwordUI;

    [SerializeField] GameObject mainUI;

    [SerializeField] GameObject excelUI;

    [SerializeField] GameObject Note;

    [SerializeField] GameObject monitor;

    private string password = "0227";
    private bool isClear = false;

    public void PassWord()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (inputField_password.text == password)
            {
                Debug.Log("맞았습니다");
                passwordUI.SetActive(false);
                mainUI.SetActive(true);
                changeBG.GetComponent<ChangeBG_Police>().RightBtn.SetActive(true);
                isClear = true;
            }
        }
    }
    public void FileOpen()
    {
        excelUI.SetActive(true);
        mainUI.SetActive(false);
    }
    public void FileClose()
    {
        excelUI.SetActive(false);
        Note.SetActive(true);
        monitor.SetActive(false);
        findEvidenceManager.GetComponent<FindEvidence_Police>().bg_center_clue1_1.interactable = true;

        if (isClear)
        {
            findEvidenceManager.GetComponent<FindEvidence_Police>().bg_center_clue1.interactable = false;
        }
    }
}
