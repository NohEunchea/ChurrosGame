using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindEvidence_feffisWheel : MonoBehaviour
{
    public GameObject NoteInteraction_feffisWheel;

    //���� ��ư - bg_center
    public Button bg_center_clue1;
    public bool bg_center_clue1_isClick = false;

    //���Ź�ư - bg_right
    public Button bg_right_clue1;
    public bool bg_right_clue1_isClick = false;


    //�˾�â
    public GameObject popUp;
    public Image popUpImg;  //�˾�â�� �� �̹��� ��ü
    public Sprite bg_center_clue1_Img; //����1 - bg_center

    public Sprite bg_right_clue1_Img; //����- - bg_right
    [Header("팝업창 닫는 속도 조절")]
    public float popUpcloseSpeed;

    // Start is called before the first frame update
    void Start()
    {
        popUp.SetActive(false);
    }

    public void bg_center_clue1Btn()
    {
        bg_center_clue1_isClick = true;


        if (bg_center_clue1_isClick)
        {
            popUpImg.sprite = bg_center_clue1_Img;
        }

        StartCoroutine("popupCorutineVer1");
        bg_center_clue1.interactable = false;

        NoteInteraction_feffisWheel.GetComponent<NoteInteraction_feffisWheel>().FindClueNoteInteraction();
        NoteInteraction_feffisWheel.GetComponent<NoteInteraction_feffisWheel>().Find_bg_center_clue1();
    }

    public void bg_right_clue1Btn()
    {
        bg_right_clue1_isClick = true;

        if (bg_right_clue1_isClick)
        {
            popUpImg.sprite = bg_right_clue1_Img;
        }

        StartCoroutine("popupCorutineVer1");
        bg_right_clue1.interactable = false;

        NoteInteraction_feffisWheel.GetComponent<NoteInteraction_feffisWheel>().FindClueNoteInteraction();
        NoteInteraction_feffisWheel.GetComponent<NoteInteraction_feffisWheel>().Find_bg_right_clue1Btn();
    }
    IEnumerator popupCorutineVer1()
    {
        popUp.SetActive(true);
        yield return new WaitForSeconds(popUpcloseSpeed);
        popUp.SetActive(false);
    }
    /*public void ClueBtninteract()
    {
        ;
    }*/
}
