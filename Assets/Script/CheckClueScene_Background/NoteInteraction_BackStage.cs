using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NoteInteraction_BackStage : MonoBehaviour
{
    public static bool iSBackStage;

    public GameObject findEvidence_BackStage;

    public bool isAllClear = false;

    public Animator animator;
    public Animator animator_NoteBtn;

    //노트 아이콘
    public GameObject noteinside;

    public GameObject goToMap;

    [SerializeField] private GameObject noteOpen;
    [SerializeField] private GameObject noteClose;

    public GameObject go_clueImg;
    public Image clueImgs;
    public Sprite[] clueImg;
    public TextMeshProUGUI clueTitle;
    public TextMeshProUGUI clueContent;

    private void Start()
    {
        noteinside.SetActive(false);
        noteOpen.SetActive(true);
        noteClose.SetActive(false);
        go_clueImg.SetActive(false);

        isAllClear = true;
    }
    public void NoteOpen()
    {
        noteinside.SetActive(true);
        noteOpen.SetActive(false);
        noteClose.SetActive(true);
    }

    public void NoteClose()
    {
        noteinside.SetActive(false);
        noteOpen.SetActive(true);
        noteClose.SetActive(false);
    }

    public void FindClueNoteInteraction()
    {
        StartCoroutine(ChangeNoteBtn());
    }

    IEnumerator ChangeNoteBtn()
    {
        animator_NoteBtn.SetBool("isChange", true);
        yield return new WaitForSeconds(0.5f);
        animator_NoteBtn.SetBool("isChange", false);
    }

    //노트 오른쪽 마우스를 대면 나오는 애니메이션
    public void NextRightHover()
    {
        animator.SetBool("isRightClick", true);
    }
    //노트 오른쪽 마우스를 때면 나오는 애니메이션
    public void NextRightHoverExit()
    {
        animator.SetBool("isRightClick", false);
    }

    bool isNext = false;
    //노트 클릭시 나오는 노트 넘김 애니메이션
    public void NextNoteClick()
    {
        if (findEvidence_BackStage.GetComponent<FindEvidence_BackStage>().bg_right_clue1_isClick == true &&
            findEvidence_BackStage.GetComponent<FindEvidence_BackStage>().bg_center_clue1_isClick == true &&
            findEvidence_BackStage.GetComponent<FindEvidence_BackStage>().bg_center_clue1_1_isClick == true)
        {
            Debug.Log("모든 증거를 찾음");
            animator.SetBool("isNext", true);
            NoteInside(false);
            goToMap.SetActive(true);
            isNext = true;
        }
    }
    //노트 내부 끄고 킴
    private void NoteInside(bool isopen)
    {
        go_clueImg.SetActive(isopen);
        clueTitle.text = " ";
        clueContent.text = " ";
    }
    public void NextNoteClickExit()
    {
        if (findEvidence_BackStage.GetComponent<FindEvidence_BackStage>().bg_right_clue1_isClick == true &&
            findEvidence_BackStage.GetComponent<FindEvidence_BackStage>().bg_center_clue1_isClick == true &&
            findEvidence_BackStage.GetComponent<FindEvidence_BackStage>().bg_center_clue1_1_isClick == true)
        {
            StartCoroutine(enumerator());
        }
    }
    IEnumerator enumerator()
    {
        animator.SetBool("isNext", false);
        clueTitle.text = " ";
        clueContent.text = " ";

        yield return new WaitForSeconds(0.4f);

        if (isNext)
        {
            if (!isCokPop && isPowder && !ischurrosReceipt)
            {
                Find_bg_center_clue1_1();
            }
            else if (isCokPop && !isPowder && !ischurrosReceipt)
            {
                Find_bg_center_clue1();
            }
            else
            {
                Find_bg_right_clue1Btn();
            }
        }
    }
    public bool isCokPop = false;
    public bool isPowder = false;
    public bool ischurrosReceipt = false;
    public void Find_bg_center_clue1()
    {
        NoteInside(true);
        clueImgs.sprite = clueImg[0];
        clueTitle.text = "콜팝";
        clueContent.text = "- 놀이공원에서 파는 콜팝이다.\r\n- 빨대에 립스틱 자국처럼 보이는 것이 묻어있다.\r\n- 립스틱의 주인은?";
        isCokPop = false;
        isPowder = true;
        ischurrosReceipt = false;
    }
    public void Find_bg_center_clue1_1()
    {
        NoteInside(true);
        clueImgs.sprite = clueImg[1];
        clueTitle.text = "통에 담긴 가루";
        clueContent.text = "- 흰 가루가 통에 담겨 있다.\r\n- 무슨 가루일까?.";
        isCokPop = false;
        isPowder = false;
        ischurrosReceipt = true;
    }
    public void Find_bg_right_clue1Btn()
    {
        NoteInside(true);
        clueImgs.sprite = clueImg[2];
        clueTitle.text = "영수증";
        clueContent.text = "- 츄러스를 구매한 영수증이다.";

        isCokPop = true;
        isPowder = false;
        ischurrosReceipt = false;
    }

    public void GoToMap()
    {
        iSBackStage = true;
        SceneManager.LoadScene("LoadScene_GoMap");
    }
}
