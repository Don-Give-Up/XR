using System;
using Fusion;
using UnityEngine;
using UnityEngine.EventSystems;

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
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return; 
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                QuestReport(hit); 
                NPCCheck(hit);
            }
        }
        
    }

    private void NPCCheck(RaycastHit hit)
    {
        NPC npc = hit.collider.GetComponent<NPC>();
        
        if (npc != null)
        {
            string npcName = npc.npcinfo.name; 
            GameEventsManager.instance.npcdialogEvents.ShowRealDialogue();
        }
        else
        {
            Debug.Log($"콜라이더 이름: {hit.collider.gameObject.name}");
        }
        /*if (hit.collider.CompareTag("NPC"))
        {  
            // npc면 대화창을 열것
            //NPCName(hit); // 이름 뭔지 알아낸다음
            
            // 대화창 뜨는 시점
            GameEventsManager.instance.npcdialogEvents.ShowRealDialogue();
            Debug.Log("대화 시작"); // 대화 시작
            //GameEventsManager.instance.npcdialogEvents.CheckDialogue(NPCName(hit.collider.GetComponent<NPC>().name));
            // 이때 실행을 해야하나? 

        }*/
    }

    private void QuestReport(RaycastHit hit)// 클릭했을 떄 리포트가 있으면 반환 할 것 
    {
        Debug.Log("퀘스트 리포터 확인");
        QuestReporter[] questReporters = hit.collider.GetComponents<QuestReporter>(); 
      
        if (questReporters != null)
        {
            foreach (var questReport in questReporters)
            {
                questReport.Report();
            }
            Debug.Log("퀘스트 리포터 확인후 보고");
        }
        else
        {
            Debug.Log("퀘스트가 없어요");
        }
    }

    private string NPCName(RaycastHit hit) // 이름을 알아와서 퀘스트 대화 시작
    {
        string npcName = hit.collider.GetComponent<NPC>().npcinfo.name; 
        Debug.Log($"NPC 이름: {npcName}"); // 일단 npc 이름만 가지고 대화 이벤트 발생 
        return npcName; 
    }
    
}