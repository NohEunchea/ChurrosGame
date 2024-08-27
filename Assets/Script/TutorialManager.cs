using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
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
        FirstDialogue.GetComponent<DialogueTrigger>().TriggerDialogue();
        isFirst = true;
    }
}
