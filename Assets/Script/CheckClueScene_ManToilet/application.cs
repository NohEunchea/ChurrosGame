using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class application : MonoBehaviour
{
    [SerializeField] private GameObject go_clueImg;
    [SerializeField] private Image clueImgs;
    [SerializeField] private Sprite[] clueImg;
    [SerializeField] private TextMeshProUGUI clueTitle;
    [SerializeField] private TextMeshProUGUI clueContent;

    public void Find_bg_center_clue1()
    {
        go_clueImg.SetActive(true);
        clueImgs.sprite = clueImg[0];
        clueTitle.text = "놀이공원 츄러스";
        clueContent.text = "- 한 입 먹은 자국이 있다.\r\n- 무언가 묻어있는거 같다.";
    }
    public void Find_bg_right_clue1Btn()
    {
        go_clueImg.SetActive(false);
        clueImgs.sprite = clueImg[1];
        clueTitle.text = "변기 물탱크 뚜껑";
        clueContent.text = "- 모서리가 깨져있다.\r\n- 알 수 없는 빨간색 자국이 있다... 핏자국 인가?";
    }
}
