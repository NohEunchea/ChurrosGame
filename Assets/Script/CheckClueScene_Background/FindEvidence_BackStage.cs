using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindEvidence_BackStage : MonoBehaviour
{
    public GameObject changeBG_BackStage;
    public GameObject noteInteraction_BackStage;

    //���� ��ư - bg_center
    public Button bg_center_clue1;
    public bool bg_center_clue1_isClick = false;

    public Button bg_center_clue1_1;
    public bool bg_center_clue1_1_isClick = false;

    //���Ź�ư - bg_right
    public Button bg_right_clue1;
    public bool bg_right_clue1_isClick = false;


    //�˾�â
    public GameObject popUp;
    public Image popUpImg;  //�˾�â�� �� �̹��� ��ü
    public Sprite bg_center_clue1_Img; //����1 - bg_center
    public Sprite bg_center_clue1_1_Img;
    public Sprite bg_right_clue1_Img; //����- - bg_right


    [Header("팝업창 닫는 속도 조절")]
    public float popUpcloseSpeed;

    void Start()
    {
        popUp.SetActive(false);
        changeBG_BackStage.GetComponent<ChangeBG_BackStage>().RightBtn.SetActive(false);
        bg_center_clue1.interactable = true;
        bg_right_clue1.interactable = false;
    }

    public void bg_center_clue1Btn()
    {
        //콜팝
        bg_center_clue1_isClick = true;


        if (bg_center_clue1_isClick)
        {
            popUpImg.sprite = bg_center_clue1_Img;
        }
        StopAllCoroutines();
        StartCoroutine("popupCorutineVer2");
        OpenNextBG();
        bg_center_clue1.interactable = false;

        noteInteraction_BackStage.GetComponent<NoteInteraction_BackStage>().FindClueNoteInteraction();
        noteInteraction_BackStage.GetComponent<NoteInteraction_BackStage>().Find_bg_center_clue1();
    }
    void OpenNextBG()
    {
        if(bg_center_clue1_isClick && bg_center_clue1_1_isClick)
        {
            changeBG_BackStage.GetComponent<ChangeBG_BackStage>().RightBtn.SetActive(true);
        }
        else
        {
            changeBG_BackStage.GetComponent<ChangeBG_BackStage>().RightBtn.SetActive(false);
        }
    }
    public void bg_center_clue1_1_Btn()
    {
        //콜팝
        bg_center_clue1_1_isClick = true;


        if (bg_center_clue1_1_isClick)
        {
            popUpImg.sprite = bg_center_clue1_1_Img;
        }

        StopAllCoroutines();
        StartCoroutine("popupCorutineVer2");
        OpenNextBG();
        bg_center_clue1_1.interactable = false;

        noteInteraction_BackStage.GetComponent<NoteInteraction_BackStage>().FindClueNoteInteraction();
        noteInteraction_BackStage.GetComponent<NoteInteraction_BackStage>().Find_bg_center_clue1_1();
    }
    

    public void bg_right_clue1Btn()
    {
        bg_right_clue1_isClick = true;

        if (bg_right_clue1_isClick)
        {
            popUpImg.sprite = bg_right_clue1_Img;
        }
        StopAllCoroutines();
        StartCoroutine("popupCorutineVer2");
        bg_right_clue1.interactable = false;

        noteInteraction_BackStage.GetComponent<NoteInteraction_BackStage>().FindClueNoteInteraction();
        noteInteraction_BackStage.GetComponent<NoteInteraction_BackStage>().Find_bg_right_clue1Btn();
    }
    
    IEnumerator popupCorutineVer2()
    {
        popUp.SetActive(true);
        yield return new WaitForSeconds(popUpcloseSpeed);
        popUp.SetActive(false);
    }
    public void ClueBtninteract()
    {
        if (changeBG_BackStage.GetComponent<ChangeBG_BackStage>().isbg_right == true)
        {
            bg_right_clue1.interactable = true;
            bg_center_clue1.interactable = false;
            bg_center_clue1_1.interactable = false;
        }
        else
        {
            bg_right_clue1.interactable = false;
            bg_center_clue1.interactable = true;
            bg_center_clue1_1.interactable = true;
        }
    }
}
