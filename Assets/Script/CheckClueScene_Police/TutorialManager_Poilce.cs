using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager_Poilce : MonoBehaviour
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
        FirstDialogue.GetComponent<DialogueTrigger_Poilce>().TriggerDialogue();
        isFirst = true;
    }
}
