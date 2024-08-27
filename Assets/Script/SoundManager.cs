using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicsourse;



    public void SetMusicVolume(float volume)
    {
        musicsourse.volume = volume;
    }
}
