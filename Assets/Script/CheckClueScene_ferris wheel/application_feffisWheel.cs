using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class application_feffisWheel : MonoBehaviour
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
        clueTitle.text = "누군가의 핸드폰";
        clueContent.text = "- 연인으로 보이는 문자 내용.\r\n- 서커스 단원과 접점이 있어보인다.";
    }
    public void Find_bg_right_clue1Btn()
    {
        go_clueImg.SetActive(false);
        clueImgs.sprite = clueImg[1];
        clueTitle.text = "커플 키링";
        clueContent.text = "- 유명한 브랜드의 유명한 커플키링이다.\r\n- 누구와 키링을 맞춘 것일까?";
    }
}
