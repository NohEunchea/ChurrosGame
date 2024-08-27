using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBG_BackStage : MonoBehaviour
{
    public GameObject findEvidence_BackStage;

    public GameObject RightBtn;

    //fade
    public Image fadeImg;
    public GameObject fade;

    //bgImg
    public GameObject bg_right;
    public bool isbg_right = false;
    public GameObject bg_center;

    // Start is called before the first frame update
    void Start()
    {
        fade.SetActive(false);
    }

    public void rightBtn()
    {
        Debug.Log("업 버튼 클릭");
        fade.SetActive(true);
        StartCoroutine("FadeInCoroutine");

        bg_right.SetActive(true);
        bg_center.SetActive(false);
        StartCoroutine("delay");

        isbg_right = true;
        findEvidence_BackStage.GetComponent<FindEvidence_BackStage>().ClueBtninteract();
        StartCoroutine("RemoveRightBtn");
    }

    IEnumerator FadeInCoroutine()
    {
        float fadeCount = 1;    //처음 알파값
        while (fadeCount > 0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.02f);
            fadeImg.color = new Color(0, 0, 0, fadeCount);
        }
    }
    IEnumerator delay()
    {
        //FadeInCoroutine 이후 시작
        yield return StartCoroutine("FadeInCoroutine");
        fade.SetActive(false);
    }
    IEnumerator RemoveRightBtn()
    {
        yield return new WaitForSeconds(0.5f);
        RightBtn.SetActive(false);
    }
}
