using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FindEvidence_Storage : MonoBehaviour
{
    public static bool isStorage;

    public AudioSource audioSource;
    public AudioClip[] audioCilp;

    //���� ��ư - bg_center
    public Button bg_center_clue1;
    public bool bg_center_clue1_isClick = false;

    public GameObject goToMap;

    //�˾�â
    public GameObject popUp;
    public Sprite bg_center_clue1_Img;
    public Image popUpImg;
    public void bg_center_clue1Btn()
    {
        bg_center_clue1_isClick = true;
        popUp.SetActive(true);

        audioSource.clip = audioCilp[0];
        audioSource.Play();
    }
    public void Next()
    {
        popUpImg.sprite = bg_center_clue1_Img;
        goToMap.SetActive(true);
    }

    public void bg_center_clue1BtnClose()
    {
        popUp.SetActive(false);
    }

    public void GoToMap()
    {
        isStorage=true;
        SceneManager.LoadScene("LoadScene_GoMap");
    }
}
