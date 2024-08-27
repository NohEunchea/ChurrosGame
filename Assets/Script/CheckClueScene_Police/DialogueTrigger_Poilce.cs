using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger_Poilce : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager_Poilce>().StartDialogue(dialogue);
    }
}
