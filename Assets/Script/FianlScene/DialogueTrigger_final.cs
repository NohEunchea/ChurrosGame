using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger_final : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager_fianl>().StartDialogue(dialogue);
    }
}
