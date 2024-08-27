using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void GoCheckClueScene_ManToilet()
    {
        SceneManager.LoadScene("CheckClueScene_ManToilet");
    }
    public void GoCheckClueScene_Storage()
    {
        SceneManager.LoadScene("CheckClueScene_Storage");
    }
    public void GoCheckClueScene_CleanRoom()
    {
        SceneManager.LoadScene("CheckClueScene_CleanRoom");
    }
    public void GoCheckClueScene_ferris_wheel()
    {
        SceneManager.LoadScene("CheckClueScene_ferris wheel");
    }
    public void GoCheckClueScene_Police()
    {
        SceneManager.LoadScene("CheckClueScene_Police");
    }
    public void GoCheckClueScene_Background()
    {
        SceneManager.LoadScene("CheckClueScene_Background");
    }
    public void GoTutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }
    public void GoMapScene()
    {
        SceneManager.LoadScene("MapScene");
    }
    public void GoFileScene()
    {
        SceneManager.LoadScene("CheckClueScene");
    }
    public void GoLoadingScene()
    {
        SceneManager.LoadScene("LoadScene");
    }
}
