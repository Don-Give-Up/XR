using System;
using UnityEngine;

public class NPCController : MonoBehaviour
{
   public UnityEngine.Events.UnityEvent onTalk;

   public void OnClick()
   {
      
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      {
         Debug.Log("스페이스바 입력");
         onTalk.Invoke();
      }
   }
}
