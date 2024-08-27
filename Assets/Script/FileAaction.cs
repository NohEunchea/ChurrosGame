using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FileAaction : MonoBehaviour
{
    public GameObject DialogueManager;

    public Image fadeImg;
    public GameObject fade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FileAaction_1();
    }

    void FileAaction_1()
    {
        if (DialogueManager.GetComponent<DialogueManager_Poilce>().isDialogueFinish == true)
        {
            Debug.Log("파일 액션 시작");
        }
    }
}
