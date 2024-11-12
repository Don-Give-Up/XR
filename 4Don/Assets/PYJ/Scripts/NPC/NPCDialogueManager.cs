using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

public class NPCDialogueManager : MonoBehaviour
{
    // 받아온 정보를 
    [SerializeField] 
    private AllDialogueEvent usedAllDialogue;

    private QuestInfoSO currentQuestInfo; 

    private bool hasTalked = true;

    private int currentDialogueNum = 0; 
    
    private KingQuest kingQuest;
    //private QuestManager questManager;
    
    private void Awake()
    {
        kingQuest = KingQuest.instance; 
        //questManager = QuestManager.instance;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.npcdialogEvents.onDialogueNumCheck += OnNPCDialogueCheck; 
        //GameEventsManager.instance.npcdialogEvents.onShowDialoge += OnShowDialogue; 
        //GameEventsManager.instance.npcdialogEvents.onStartDialoge += OnNPCDialogueFinish; 
    }

    private void OnDisable()
    {
        GameEventsManager.instance.npcdialogEvents.onDialogueNumCheck -= OnNPCDialogueCheck; 
        //GameEventsManager.instance.npcdialogEvents.onShowDialoge -= OnShowDialogue; 
        //GameEventsManager.instance.npcdialogEvents.onStartDialoge -= OnNPCDialogueFinish; 
    }

    // 퀘스트의 상태를 확인하고 해댕 npc 대화 시작 가능 여부를 판단
    public void OnNPCDialogueCheck(string npcName) // 어떤 대화를 해야 할 지 선택하는 과정, npc 이름을 가지고 들어옴
    {
        // 클릭했을 때 NPC의 이름을 반환하고 
        // 현재 퀘스트 정보를 가지고 와서 
        // 이 친구가 말할 수 있는 지? 어떤 내용을 말해야하는 지 대답한다. 

        currentQuestInfo = kingQuest.GetQuestInfo(); // 현재 퀘스트 정보 가저옴
        Vector2 dialogueNum = Vector2.zero;
        
        switch (hasTalked)
        {
            case true:
                // 전 대화를 나누었어! 
                // 후 대화를 나눌차례
                dialogueNum = currentQuestInfo.dialogueLine[1];
                hasTalked = false;
                break;
            case false:
                // 전 대화를 나누지 않았어.
                // 후 대화를 나눌차례 
                dialogueNum = currentQuestInfo.dialogueLine[0];
                hasTalked = true; 
                break;
        }
        
        Debug.Log($"대화라인:{(int)dialogueNum.x}, {(int)dialogueNum.y}");

        currentDialogueNum = (int)dialogueNum.x;
        GetDialogueData(dialogueNum);
        OnShowDialogue(currentDialogueNum);
    }

    // 필요한 대화 저장하기
    private Dictionary<int, AllDialogue> GetDialogueData(Vector2 dialogueNum)
    {
        usedAllDialogue.alldialogues = DataBaseManager.instance.GetAllDialogues(dialogueNum);
        return usedAllDialogue.alldialogues;
    }

    private void OnShowDialogue(int currentDial)
    {
        string npcName = usedAllDialogue.alldialogues[currentDial].name;
        string npcText = usedAllDialogue.alldialogues[currentDial].content;
        
        Debug.Log($"{npcText}");
 
    }

    // 버튼 클릭 받으면 인덱스 하나 움직인 다음에 OnShowDialogue 부르기 
    public void MoveNext()
    {
        // 현재 라인의 상태에 따라 어디로 옮길 지, 어떤 행동을 할 지 등등 구별해 놓기 

        /*
        if ()
        {
            
        }
        else if ()
        {
            
        }
        else
        {
        }
        */

        currentDialogueNum++; 
        OnShowDialogue(currentDialogueNum);
    }

    public void OnNPCDialogueAdvance()
    {
        
    }

    public void OnNPCDialogueFinish()
    {
        
    }
    
    

}
