using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class FindEvidence_Police : MonoBehaviour
{
    public GameObject ChangeBGManager;
    public GameObject PCInteract;

    // bg_center
    public Button bg_center_clue1;
    [SerializeField] GameObject monitor;    //PC 배경화면
    public bool bg_center_clue1_isClick = false;


    public Button bg_center_clue1_1;
    public bool bg_center_clue1_1_isClick = false;

    //bg_right
    public Button bg_right_clue1;
    public bool bg_right_clue1_isClick = false;


    [SerializeField] GameObject noteinside;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI content;

    [SerializeField] GameObject Note;

    [SerializeField] private GameObject noteOpen;
    [SerializeField] private GameObject noteClose;

    public GameObject popUp;
    public Image popUpImg;  //�˾�â�� �� �̹��� ��ü
    public Sprite bg_center_clue1_Img; //����1 - bg_center

    [Header("팝업창 닫는 속도 조절")]
    public float popUpcloseSpeed;
    // Start is called before the first frame update
    void Start()
    {
        bg_center_clue1.interactable = true;
        bg_right_clue1.interactable = false;
        bg_center_clue1_1.interactable = true;
        monitor.SetActive(false);
        ChangeBGManager.GetComponent<ChangeBG_Police>().RightBtn.SetActive(false);
    }

    public void bg_center_clue1Btn()
    {
        //PC 화면
        bg_center_clue1_isClick = true;
        monitor.SetActive(true);
        Note.SetActive(false);

        //bg_center_clue1.interactable = false;
        bg_center_clue1_1.interactable = false;

        //ChangeBGManager.GetComponent<ChangeBG_Police>().RightBtn.SetActive(true);
    }
    public void bg_center_clue1_1_btn()
    {
        bg_center_clue1_1_isClick = true;
        StartCoroutine(popupCorutineVer1());
        popUpImg.sprite = bg_center_clue1_Img;
        //bg_center_clue1_1.interactable= false;
    }
    IEnumerator popupCorutineVer1()
    {
        popUp.SetActive(true);
        yield return new WaitForSeconds(popUpcloseSpeed);
        popUp.SetActive(false);
    }
    public void bg_right_clue1Btn()
    {
        bg_right_clue1_isClick = true;

        //파일 열기
        SceneManager.LoadScene("FileScene");
    }

    public void NoteOpne()
    {
        noteinside.SetActive(true);
        noteOpen.SetActive(false);
        noteClose.SetActive(true);
        if (bg_center_clue1_isClick)
        {
            title.text = "사건 번호 /  검거여부  / 비고\r\n";
            content.text =
                "030611 - 사710  / 미검거  / 7팀_최희원\r\n";
        }
        else
        {
            title.text = " ";
            content.text = " ";
        }
    }
    public void NoteClose()
    {
        noteinside.SetActive(false);
        noteOpen.SetActive(true);
        noteClose.SetActive(false);
    }

    public void ClueBtninteract()
    {
        if (ChangeBGManager.GetComponent<ChangeBG_Police>().isbg_right)
        {
            Debug.Log("����̹��� ü���� �Ϸ�");
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
