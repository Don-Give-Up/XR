using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
   public AudioSource audioSource;

   public void PlayAuido()
   {
      audioSource.Play();
   }
   
}
