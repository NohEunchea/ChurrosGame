using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NoteInteraction_feffisWheel : MonoBehaviour
{
    public static bool isfeffisWheel;

    public GameObject FindEvidence_feffisWheel;

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
        if (FindEvidence_feffisWheel.GetComponent<FindEvidence_feffisWheel>().bg_right_clue1_isClick == true && 
            FindEvidence_feffisWheel.GetComponent<FindEvidence_feffisWheel>().bg_center_clue1_isClick == true)
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
        if (FindEvidence_feffisWheel.GetComponent<FindEvidence_feffisWheel>().bg_right_clue1_isClick == true && 
            FindEvidence_feffisWheel.GetComponent<FindEvidence_feffisWheel>().bg_center_clue1_isClick == true)
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
            if (iskatalkLoveMan && !iskeyring)
            {
                Find_bg_center_clue1();
            }
            else
            {
                Find_bg_right_clue1Btn();
            }
        }
    }
    bool iskatalkLoveMan = false;
    bool iskeyring = false;
    public void Find_bg_center_clue1()
    {
        NoteInside(true);
        clueImgs.sprite = clueImg[0];
        clueTitle.text = "누군가의 핸드폰";
        clueContent.text = "- 연인으로 보이는 문자 내용.\r\n- 서커스 단원과 접점이 있어보인다.";
        iskatalkLoveMan = false;
        iskeyring = true;
    }
    public void Find_bg_right_clue1Btn()
    {
        NoteInside(true);
        clueImgs.sprite = clueImg[1];
        clueTitle.text = "커플 키링";
        clueContent.text = "- 유명한 브랜드의 유명한 커플키링이다.\r\n- 누구와 키링을 맞춘 것일까?";

        iskatalkLoveMan = true;
        iskeyring = false;
    }

    public void GoToMap()
    {
        isfeffisWheel = true;
        SceneManager.LoadScene("LoadScene_GoMap");
    }
}
