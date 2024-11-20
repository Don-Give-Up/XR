using System;
using Fusion;
using UnityEngine;

public class PlayerInterAction : MonoBehaviour
{
   public float rayDistance = 10f;
   
   private void Update()
   {
      // Camera.main이 null인지 확인
      if (Camera.main == null)
      {
         Debug.LogError("Main Camera not found!");
         return;  // Camera가 없으면 Update에서 더 이상 진행하지 않음
      }

      if (Input.GetMouseButtonDown(0))
      {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         RaycastHit hit;

         if (Physics.Raycast(ray, out hit, rayDistance))
         {
            NPCCheck(hit);
         }
      }
   }

   private void NPCCheck(RaycastHit hit)
   {
      if (hit.collider.CompareTag("NPC"))
      {  
         // npc면 대화창을 열것
         //GameEventsManager.instance.npcdialogEvents.CheckDialogue(NPCName(hit.collider.GetComponent<NPC>().name));
      }
      else
      {
         Debug.Log("NPC 아님"); // 일단 되긴 하는 듯 일단 npc 아니라고 함 ㅋㅋ
      }
   }

   private string NPCName(string npcName) // 이름을 알아와서 퀘스트 대화 시작
   {
      Debug.Log($"NPC 이름: {npcName}"); // 일단 npc 이름만 가지고 대화 이벤트 발생 
      return npcName; 
   }
}
