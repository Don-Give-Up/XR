using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    
    public void SoundPlay()
    {
        audioSource.Play();
    }
}
