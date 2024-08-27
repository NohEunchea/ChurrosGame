using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class application_BackStage : MonoBehaviour
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
        clueTitle.text = "콜팝";
        clueContent.text = "- 놀이공원에서 파는 콜팝이다.\r\n- 빨대에 립스틱 자국처럼 보이는 것이 묻어있다.\r\n- 립스틱의 주인은?";
    }
    public void Find_bg_center_clue1_1()
    {
        go_clueImg.SetActive(false);
        clueImgs.sprite = clueImg[1];
        clueTitle.text = "통에 담긴 가루";
        clueContent.text = "- 흰 가루가 통에 담겨 있다.\r\n- 무슨 가루일까?.";
    }
    public void Find_bg_right_clue1Btn()
    {
        go_clueImg.SetActive(false);
        clueImgs.sprite = clueImg[2];
        clueTitle.text = "영수증";
        clueContent.text = "- 츄러스를 구매한 영수증이다.";

    }
}
