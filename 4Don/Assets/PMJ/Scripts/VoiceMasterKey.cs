using System;
using Photon.Voice.Unity;
using UnityEngine;

public class VoiceMasterKey : MonoBehaviour
{
   public Recorder recorder;
 

   private void Start()
   {
      if (recorder == null)
      {
         recorder = FindObjectOfType<Recorder>();
         Debug.Log("레코더 찾았다");
      }
   }
   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Tab))
      {
         if (recorder != null)
         {
            recorder.TransmitEnabled = true;
            Debug.Log("마이크 켜짐 ㅎㅎ");
         }
      }

      if (Input.GetKeyUp(KeyCode.Tab))
      {
         if (recorder != null)
         {
            recorder.TransmitEnabled = false;
            Debug.Log("마이크 꺼짐 ㅠㅠ");
         }
      }

      
   }
   
}
