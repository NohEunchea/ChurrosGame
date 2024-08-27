using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager_fianl : MonoBehaviour
{
    public GameObject popUp;
    public GameObject nextPopUp;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    private Queue<string> sentences;

    public bool isDialogueFinish = false;

    public GameObject tutorialManager;

    public GameObject go_Dialogue;


    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
        tutorialManager = GameObject.Find("TutorialManager");
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
        Debug.Log("��ȭ ����");

        isDialogueFinish = true;


        popUp.SetActive(true);
    }

    public void NextPopUp()
    {
        popUp.SetActive(false);
        nextPopUp.SetActive(true);
    }
    public void Fianl()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit(); // 어플리케이션 종료
#endif

    }
}
