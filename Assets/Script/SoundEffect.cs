using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioCilp;

    public GameObject nextBtn;

    //TutorialScene
    public void OnWalkSound()
    {
        audioSource.clip = audioCilp[0];
        audioSource.Play();

        nextBtn.SetActive(false);
    }
    public void OnThunderSound()
    {
        audioSource.clip = audioCilp[1];
        audioSource.Play();

        nextBtn.SetActive(false);
    }
    public void OnRunSound()
    {
        audioSource.clip = audioCilp[2];
        audioSource.Play();

        nextBtn.SetActive(false);
    }
    public void SoundStop()
    {
        audioSource.Stop();

        nextBtn.SetActive(true);
    }
    //CheckClueScene_ManToilet
    public void NoteSound()
    {
        audioSource.clip = audioCilp[0];
        audioSource.Play();
    }
    public void HintSound()
    {
        audioSource.clip = audioCilp[0];
        audioSource.Play();
    }
    public void BookSound()
    {
        audioSource.clip = audioCilp[1];
        audioSource.Play();

    }
    public void SoundStop_Note()
    {
        audioSource.Stop();
    }
}
