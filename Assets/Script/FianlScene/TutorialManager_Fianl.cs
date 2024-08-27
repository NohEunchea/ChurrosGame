using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager_Fianl : MonoBehaviour
{
    public GameObject FirstDialogue;

    public bool isFirst = false;
    public bool isSecond = false;
    private void Awake()
    {
        FirstDialogue = GameObject.Find("FirstDialogue");
    }

    private void Start()
    {
        FirstDialogue.GetComponent<DialogueTrigger_final>().TriggerDialogue();
        isFirst = true;
    }
}
