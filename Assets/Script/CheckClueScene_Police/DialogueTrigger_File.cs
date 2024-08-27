using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger_File : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager_File>().StartDialogue(dialogue);
    }
}
