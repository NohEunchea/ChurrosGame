using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NoteInteraction : MonoBehaviour
{
    public static bool iSManToilet;

    public GameObject FindEvidence;

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
        if(FindEvidence.GetComponent<FindEvidence>().bg_right_clue1_isClick == true && FindEvidence.GetComponent<FindEvidence>().bg_center_clue1_isClick == true)
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
        if (FindEvidence.GetComponent<FindEvidence>().bg_right_clue1_isClick == true && FindEvidence.GetComponent<FindEvidence>().bg_center_clue1_isClick == true)
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
            if (isToilet && !ischurros)
            {
                Find_bg_center_clue1();
            }
            else
            {
                Find_bg_right_clue1Btn();
            }
        }
    }
    bool isToilet = false;
    bool ischurros = false;
    public void Find_bg_center_clue1()
    {
        NoteInside(true);
        clueImgs.sprite = clueImg[0];
        clueTitle.text = "놀이공원 츄러스";
        clueContent.text = "- 한 입 먹은 자국이 있다.\r\n- 무언가 묻어있는거 같다.";
        isToilet = false;
        ischurros = true;
    }
    public void Find_bg_right_clue1Btn()
    {
        NoteInside(true);
        clueImgs.sprite = clueImg[1];
        clueTitle.text = "변기 물탱크 뚜껑";
        clueContent.text = "- 모서리가 깨져있다.\r\n- 알 수 없는 빨간색 자국이 있다... 핏자국 인가?";

        isToilet = true;
        ischurros = false;
    }

    public void GoToMap()
    {
        iSManToilet = true;
        SceneManager.LoadScene("LoadScene_GoMap");
    }
}
