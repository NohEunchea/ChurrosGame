using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager_Poilce : MonoBehaviour
{
    [SerializeField] private GameObject clue;
    [SerializeField] private GameObject Changebtn;
    [SerializeField] private GameObject Note;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    private Queue<string> sentences;

    public bool isDialogueFinish = false;

    //public GameObject tutorialManager;

    public GameObject go_Dialogue;

    private void OtherGameObject(bool isActive)
    {
        clue.SetActive(isActive);
        Changebtn.SetActive(isActive);
        Note.SetActive(isActive);
    }

    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
        OtherGameObject(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letters in sentence.ToCharArray())
        {
            dialogueText.text += letters;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        
        go_Dialogue.SetActive(false);
        Debug.Log("대사 끝");
        isDialogueFinish = true;
        OtherGameObject(true);
    }
}
