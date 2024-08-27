using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Image fadeImg;
    public GameObject fade;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    private Queue<string> sentences;

    public bool isDialogueFinish = false;

    public GameObject tutorialManager;

    public GameObject go_Dialogue;

    public GameObject MoveScene;

    //public GameObject nextBtn;

    public Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
        tutorialManager = GameObject.Find("TutorialManager");

        fade.SetActive(false);
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
        else if (sentences.Count == 1)
        {
            //animator.SetBool("isRun", true);
        }
        else if (sentences.Count == 2)
        {
            animator.SetBool("isThunder", true);
        }
        else if (sentences.Count == 3)
        {
            ;
        }
        else if (sentences.Count == 4)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            ;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letters in sentence.ToCharArray())
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

        fade.SetActive(true);

        StartCoroutine("FadeoutCoroutine");
        
    }

    IEnumerator FadeoutCoroutine()
    {
        animator.SetBool("isRun", true);

        yield return new WaitForSeconds(2.5f); ;

        float fadeCount = 0;    //처음 알파값

        while (fadeCount < 1)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.02f);
            fadeImg.color = new Color(0, 0, 0, fadeCount);


            if (fadeCount > 1)
            {
                Debug.Log("다른 씬으로 이동");
                MoveScene.GetComponent<MoveScene>().GoCheckClueScene_Police();
            }
        }
    }
}
