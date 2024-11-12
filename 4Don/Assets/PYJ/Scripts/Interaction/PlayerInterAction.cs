using System;
using UnityEngine;

public class PlayerInterAction : MonoBehaviour
{
   public float rayDistance = 10f;
   
   private void Update()
   {
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
         GameEventsManager.instance.npcdialogEvents.StartDialoge(NPCName(hit.collider.GetComponent<NPC>().name));
      }
      else
      {
         Debug.Log("NPC 아님");
      }
   }

   private string NPCName(string npcName) // 이름을 알아와서 퀘스트 대화 시작
   {
      Debug.Log($"NPC 이름: {npcName}"); // 일단 npc 이름만 가지고 대화 이벤트 발생 
      return npcName; 
   }
}
