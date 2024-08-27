using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBtnManager : MonoBehaviour
{
    [SerializeField] private GameObject HelpAndOption;
    [SerializeField] private GameObject CloseBtn;

    [SerializeField] private GameObject Quit;

    private void Awake()
    {
        HelpAndOption.SetActive(false);
        CloseBtn.SetActive(false);
    }
    public void HelpAndOptionBtn()
    {
        HelpAndOption.SetActive(true);
        CloseBtn.SetActive(true);
    }
    public void CloseBtnBtn()
    {
        HelpAndOption.SetActive(false);
        CloseBtn.SetActive(false);
    }
    public void QuitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit(); // 어플리케이션 종료
#endif
    }
}
