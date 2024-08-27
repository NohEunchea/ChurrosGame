using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FindEvidence : MonoBehaviour
{
    GameObject changeBG;
    public GameObject noteInteraction;

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

    private void Awake()
    {
        changeBG = GameObject.Find("ChangeBGManager");
    }
    // Start is called before the first frame update
    void Start()
    {
        popUp.SetActive(false);
        changeBG.GetComponent<ChangeBG>().RightBtn.SetActive(false);
        bg_center_clue1.interactable = true;
        bg_right_clue1.interactable = false;
    }

    public void bg_center_clue1Btn()
    {
        //츄러스
        bg_center_clue1_isClick = true;


        if (bg_center_clue1_isClick)
        {
            popUpImg.sprite = bg_center_clue1_Img;
        }

        StartCoroutine("popupCorutineVer1");
        bg_center_clue1.interactable = false;

        noteInteraction.GetComponent<NoteInteraction>().FindClueNoteInteraction();
        noteInteraction.GetComponent<NoteInteraction>().Find_bg_center_clue1();
    }

    public void bg_right_clue1Btn()
    {
        bg_right_clue1_isClick = true;

        if (bg_right_clue1_isClick)
        {
            popUpImg.sprite = bg_right_clue1_Img;
        }

        StartCoroutine("popupCorutineVer2");
        bg_right_clue1.interactable = false;

        noteInteraction.GetComponent<NoteInteraction>().FindClueNoteInteraction();
        noteInteraction.GetComponent<NoteInteraction>().Find_bg_right_clue1Btn();
    }
    IEnumerator popupCorutineVer1()
    {
        popUp.SetActive(true);
        changeBG.GetComponent<ChangeBG>().RightBtn.SetActive(false);
        yield return new WaitForSeconds(popUpcloseSpeed);
        popUp.SetActive(false);
        changeBG.GetComponent<ChangeBG>().RightBtn.SetActive(true);
    }
    IEnumerator popupCorutineVer2()
    {
        popUp.SetActive(true);
        yield return new WaitForSeconds(popUpcloseSpeed);
        popUp.SetActive(false);
    }
    public void ClueBtninteract()
    {
        if (changeBG.GetComponent<ChangeBG>().isbg_right)
        {
            Debug.Log("����̹��� ü���� �Ϸ�");
            bg_right_clue1.interactable = true;
            bg_center_clue1.interactable = false;
        }
        else
        {
            bg_right_clue1.interactable = false;
            bg_center_clue1.interactable = true;
        }
    }
}
