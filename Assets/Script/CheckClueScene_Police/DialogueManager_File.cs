using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager_File : MonoBehaviour
{
    public Image fadeImg;
    public GameObject fade;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    private Queue<string> sentences;

    public bool isDialogueFinish = false;

    //public GameObject tutorialManager;

    public GameObject go_Dialogue;


    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
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
        StartCoroutine("FadeoutCoroutine");
        StartCoroutine("delay");
    }
    IEnumerator FadeoutCoroutine()
    {
        fade.SetActive(true);
        float fadeCount = 0;    //처음 알파값

        while (fadeCount < 1)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.02f);
            fadeImg.color = new Color(0, 0, 0, fadeCount);
        }
    }
    IEnumerator delay()
    {
        //FadeInCoroutine 이후 시작
        yield return StartCoroutine("FadeoutCoroutine");
        SceneManager.LoadScene("LoadScene_GoMap");
    }
}
