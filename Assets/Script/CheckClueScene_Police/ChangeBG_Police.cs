using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBG_Police : MonoBehaviour
{
    public GameObject FindEvidenceManager;

    public GameObject RightBtn;

    //fade
    public Image fadeImg;
    public GameObject fade;

    //bgImg
    public GameObject bg_right;
    public bool isbg_right = false;
    public GameObject bg_center;

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
        FindEvidenceManager.GetComponent<FindEvidence_Police>().ClueBtninteract();
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
        yield return null;
        RightBtn.SetActive(false);
    }
}
