using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPCDialogueManager : MonoBehaviour
{
    // 받아온 정보를 
    private AllDialogueEvent usedDialogue;

    [SerializeField] private AllDialogueEvent usedAllDialogue;

    private QuestManager questManager;

    private void Awake()
    {
        questManager = QuestManager.instance;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.npcdialogEvents.onDialogueNumCheck += OnNPCDialogueCheck; 
        //GameEventsManager.instance.npcdialogEvents.onStartDialoge += OnNPCDialogueAdvance; 
        //GameEventsManager.instance.npcdialogEvents.onStartDialoge += OnNPCDialogueFinish; 
    }

    private void OnDisable()
    {
        GameEventsManager.instance.npcdialogEvents.onDialogueNumCheck -= OnNPCDialogueCheck; 
        //GameEventsManager.instance.npcdialogEvents.onStartDialoge -= OnNPCDialogueAdvance; 
        //GameEventsManager.instance.npcdialogEvents.onStartDialoge -= OnNPCDialogueFinish; 
    }

    // 퀘스트의 상태를 확인하고 해댕 npc 대화 시작 가능 여부를 판단
    public void OnNPCDialogueCheck(string npcName) // 어떤 대화를 해야 할 지 선택하는 과정, npc 이름을 가지고 들어옴
    {
       // can_Start 인 친구 중
       // npc 이름이 들어왔을 떄
       // 1. 일단 지금 요구조건을 충족 못 한 상태가 아닌 그리고 끝난 상태가 아닌 퀘스트 정보를 가지고 온다. 
       // 2. 관련 npc 가 맞는지 확인한다. 
       // 3. 상태에 따라 대화를 출력허고 
       // 4. 퀘스트의 상태를 업데이트 하는 쪽에 연락한다. 
       Quest currentQuest = null;
       
       //한 바퀴 쭉 돌면서 어떤 퀘스트의 대화를 할 지 하나 뽑기
       foreach (Quest quest in questManager.questDic.Values)
       {
           if (quest.info.name == npcName)
           {
               if (quest.state != QuestState.REQUIREMENTS_NOT_MET && quest.state != QuestState.FINISHED)
               {
                   currentQuest = quest; // 일단 하나만 찾아서 나가는 거 // 어떤 엔피시가 어떤 대화를 하고 싶어하는지
                   Debug.Log($"NPC:{npcName}, 퀘스트 이름: {quest.info.name}, 상태: {quest.state}");
                   break; 
               }
           }
       }

       //하나 뽑은 퀘스트의 상태에 따라 어떤 대화 출력할지 정하기
       Vector2 dialogueNum = Vector2.zero;
       switch (currentQuest.state)
       {
           case QuestState.CAN_START: // 전 
               dialogueNum = currentQuest.info.dialogueLine[0];
               break;
           case QuestState.IN_PROGRESS: // 중
               dialogueNum = currentQuest.info.dialogueLine[1];
               break;
           case QuestState.CAN_FINISH: // 후
               dialogueNum = currentQuest.info.dialogueLine[2];
               break;
       }
       
       
    }

    public void OnNPCDialogueAdvance()
    {
        
    }

    public void OnNPCDialogueFinish()
    {
        
    }
    
    

}
