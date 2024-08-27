using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MouseEvent : MonoBehaviour
{
    //public GameObject NoteInteraction_BackStage;

    [SerializeField] GameObject Backstage;
    [SerializeField] GameObject Storage;
    [SerializeField] GameObject Toilet;
    [SerializeField] GameObject CleanRoom;
    [SerializeField] GameObject Ferriswheel;

    [SerializeField] GameObject Selectcriminal;
    public TextMeshProUGUI messageText;


    public TextMeshProUGUI showinfo;
    public AudioSource audioSource;
    public AudioClip[] audioCilp;

    private void Awake()
    {
        showinfo.text = " ";
        bool myBoolValue_Storage = FindEvidence_Storage.isStorage;
        Debug.Log("Storage : "+ myBoolValue_Storage);

        bool myBoolValue_CleanRoom = NoteInteraction_CleanRoom.isCleanRoom;
        Debug.Log("CleanRoom : " + myBoolValue_CleanRoom);

        bool myBoolValue_feffisWheel = NoteInteraction_feffisWheel.isfeffisWheel;
        Debug.Log("CleanRoom : " + myBoolValue_feffisWheel);

        bool myBoolValue_Toilet = NoteInteraction.iSManToilet;
        Debug.Log("CleanRoom : " + myBoolValue_Toilet);

        bool myBoolValue_BackStage = NoteInteraction_BackStage.iSBackStage;
        Debug.Log("CleanRoom : " + myBoolValue_BackStage);

        if (myBoolValue_Storage)
        {
            Storage.SetActive(false);
        }
        if (myBoolValue_CleanRoom)
        {
            CleanRoom.SetActive(false);
        }
        if (myBoolValue_feffisWheel)
        {
            Ferriswheel.SetActive(false);
        }
        if (myBoolValue_Toilet)
        {
            Toilet.SetActive(false);
        }
        if (myBoolValue_BackStage)
        {
            Backstage.SetActive(false);
        }

        if(myBoolValue_Storage && myBoolValue_CleanRoom && myBoolValue_feffisWheel && myBoolValue_Toilet && myBoolValue_BackStage)
        {
            Selectcriminal.SetActive(true);
        }
    }

    // 버튼 클릭 시 호출되는 함수
    public void OnCriSeoButtonClick()
    {
        // 부모 오브젝트가 지정되어 있으면
        if (Selectcriminal != null)
        {
            SceneManager.LoadScene("FianlScene");
            // 부모 오브젝트를 비활성화
            Selectcriminal.SetActive(false);
        }
        else
        {
            // 오류 처리: 부모 오브젝트가 지정되어 있지 않을 때
            Debug.LogError("Parent object not assigned!");
        }
    }

    // CriYuButton 클릭 시 호출되는 함수
    public void OnCriYuButtonClick()
    {
        if (messageText != null)
        {
            messageText.text = "범인이 아닙니다.";
        }
        else
        {
            Debug.LogError("UI Text not assigned!");
        }
    }
    public void HoverToilet()
    {
        showinfo.text = " ";
        Debug.Log("toilet");
        showinfo.text = "화장실";
        audioSource.clip = audioCilp[0];
        audioSource.Play();
    }
    public void HoverStorage()
    {
        showinfo.text = " ";
        Debug.Log("Storage");
        showinfo.text = "분실물 센터";
        audioSource.clip = audioCilp[0];
        audioSource.Play();
    }
    public void HoverBackstage()
    {
        showinfo.text = " ";
        Debug.Log("Backstage");
        showinfo.text = "백스테이지";
        audioSource.clip = audioCilp[0];
        audioSource.Play();
    }
    public void HoverCleanRoom()
    {
        showinfo.text = " ";
        Debug.Log("CleanRoom");
        showinfo.text = "청소실";
        audioSource.clip = audioCilp[0];
        audioSource.Play();
    }
    public void HoverFerriswheel()
    {
        showinfo.text = " ";
        Debug.Log("Ferriswheel");
        showinfo.text = "관람차";
        audioSource.clip = audioCilp[0];
        audioSource.Play();
    }
    public void HoverExit()
    {
        showinfo.text = " ";
    }
    public void ClickToilet()
    {
        Debug.Log("화장실 씬으로 이동");
        SceneManager.LoadScene("LoadScene_GoManToilet");
    }
    public void ClickStorage()
    {
        Debug.Log("분실물 보관소 씬으로 이동");
        SceneManager.LoadScene("LoadScene_GoStorage");

    }
    public void ClickBackstage()
    {
        //ickBackstage = true;
        Debug.Log("백스테이지 씬으로 이동");
        SceneManager.LoadScene("LoadScene_GoBackStage");
    }
    public void ClickCleanRoom()
    {
        Debug.Log("청소룸 씬으로 이동");
        SceneManager.LoadScene("LoadScene_GoCleanRoom");
    }
    public void ClickFerriswheel()
    {
        Debug.Log("관람차 씬으로 이동");
        SceneManager.LoadScene("LoadScene_Goferris wheel");
    }
}
