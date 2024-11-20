using System;
using UnityEngine;

public class WelcomQuest : QuestStep
{
    private bool hasTalked = false;
    private int dialogueStep = 0;
    private Vector2 dialogurNum;
    
    //마을 촌장과 대화하기

    public void Start()
    {
        if (!hasTalked)
        {
            StartQuest();
        }
    }

    private void StartQuest() // 시작하자 마자 이벤트 진행 가능 상태 상태 
    {
        Debug.Log("마을 이장과 대화하는 퀘스트 시작");
        ChangeState("CAN_FINISH", "마을 이장과 대화하여 퀘스트를 완료하세요!");
        // 받아오기
    }

    public void OnDialogueStart()// npc 클릭하면 이거 실행, 이거 실행되면 이거의 
    {
        if (name == "촌장" && !hasTalked)
        {
            hasTalked = true;
            
            ChangeState("IN_PROGRESS", "마을 이장과 대화 중");
        } 
        
        Debug.Log("퀘스트 진행중");
        ChangeState("IN_PROGRESS", "마을 이장과 대화 중");
    }
    
    private void OnDialogueFinish()
    {
        ChangeState("FINISHED", "마을 이장과 대화 완료함");
        FinishQuestStep();
    }
    
    protected override void SetQuestStepState(string state)
    {
        Debug.Log($"SetQuestStepState : {state}");

        switch (state)
        {
            case "CAN_START":
                break;
            case "IN_PROGRESS":
                break;
            case "CAN_FINISH":
                break;
            case "FINISHED":
                break;
            
        }
    }
    
}
