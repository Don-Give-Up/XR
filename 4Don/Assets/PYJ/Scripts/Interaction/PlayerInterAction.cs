using System;
using Fusion;
using UnityEngine;

public class PlayerInterAction : MonoBehaviour
{
    //public UnityEngine.Events.UnityEvent onTalk;
   
    private float rayDistance = 10f;
   
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
            QuestReport(hit); // 가지고 있는 보고 함 하고
            NPCName(hit); // 이름 뭔지 알아낸다음
            
            // 대화창 뜨는 시점
            GameEventsManager.instance.npcdialogEvents.ShowRealDialogue(); 
            Debug.Log("대화 시작"); // 대화 시작
            //GameEventsManager.instance.npcdialogEvents.CheckDialogue(NPCName(hit.collider.GetComponent<NPC>().name));
            // 이때 실행을 해야하나? 

        }
        else
        {
            Debug.Log("NPC 아님"); // 일단 되긴 하는 듯 일단 npc 아니라고 함 ㅋㅋ
        }
    }

    private void QuestReport(RaycastHit hit)
    {
        QuestReporter questReporter = hit.collider.GetComponent<QuestReporter>();
      
        if (questReporter != null)
        {
            questReporter.WaitForNPCInteractionAsync();
            
        }
        else
        {
            Debug.Log("NPC 지만 있는 퀘스트가 없어요");
        }
    }

    private string NPCName(RaycastHit hit) // 이름을 알아와서 퀘스트 대화 시작
    {
        string npcName = hit.collider.GetComponent<NPC>().npcinfo.name; 
        Debug.Log($"NPC 이름: {npcName}"); // 일단 npc 이름만 가지고 대화 이벤트 발생 
        return npcName; 
    }
    
}