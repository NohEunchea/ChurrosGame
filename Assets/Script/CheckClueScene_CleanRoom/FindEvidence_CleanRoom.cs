using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindEvidence_CleanRoom : MonoBehaviour
{
    public GameObject NoteInteraction_CleanRoom;

    public AudioSource audioSource;
    public AudioClip[] audioCilp;

    //���� ��ư - bg_center
    public Button bg_center_clue1;
    public bool bg_center_clue1_isClick = false;

    //���Ź�ư - bg_right
    public Button bg_right_clue1;
    public bool bg_right_clue1_isClick = false;


    //�˾�â
    public GameObject popUp;
    public GameObject popUpUserInfo;
    public Image popUpImg;  //�˾�â�� �� �̹��� ��ü
    public Sprite bg_center_clue1_Img; //����1 - bg_center

    [Header("팝업창 닫는 속도 조절")]
    public float popUpcloseSpeed;

    void Start()
    {
        popUp.SetActive(false);
    }

    public void bg_center_clue1Btn()
    {
        //츄러스
        bg_center_clue1_isClick = true;


        if (bg_center_clue1_isClick)
        {
            popUpImg.sprite = bg_center_clue1_Img;
        }

        StartCoroutine("popupCorutineVer2");
        bg_center_clue1.interactable = false;

        NoteInteraction_CleanRoom.GetComponent<NoteInteraction_CleanRoom>().FindClueNoteInteraction();
        NoteInteraction_CleanRoom.GetComponent<NoteInteraction_CleanRoom>().Find_bg_center_clue1();
        NoteInteraction_CleanRoom.GetComponent<NoteInteraction_CleanRoom>().isAllCheck();
    }

    IEnumerator popupCorutineVer2()
    {
        popUp.SetActive(true);
        yield return new WaitForSeconds(popUpcloseSpeed);
        popUp.SetActive(false);
    }

    public void bg_right_clue1Btn()
    {
        bg_right_clue1_isClick = true;

        popUpUserInfo.SetActive(true);

        audioSource.clip = audioCilp[0];
        audioSource.Play();

        NoteInteraction_CleanRoom.GetComponent<NoteInteraction_CleanRoom>().isAllCheck();
    }
    public void popUpUserInfoClose()
    {
        popUpUserInfo.SetActive(false);
    }
}
