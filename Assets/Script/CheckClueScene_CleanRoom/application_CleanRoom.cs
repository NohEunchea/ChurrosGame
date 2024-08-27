using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class application_CleanRoom : MonoBehaviour
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
        clueTitle.text = "농약";
        clueContent.text = "- 무색 무취의 고독성으로 유명한 농약.\r\n- 누군가 가져간거 같다.";
    }
}
