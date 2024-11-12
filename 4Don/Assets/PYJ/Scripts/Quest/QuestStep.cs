using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour // 퀘스트 단계를 처리 / 각 단께가 완료되었는지 확인하고 기본적인 행동을 정의, 구체적인 동작을 정의하진 않고 자식 클래스에 맡기고 있음  
{//추상 클래스로 선언, 직접 인스턴스를 생성할 수 없고, 반드시 상속받는 구체적인 클래스에서 인스턴스를 생성해서 사용해야 함
    //퀘스트 단계의 공통된 로직은 정의하고, 각 단계의 구체적인 동작은 자식 클래스에 맡기기 위함 
    // 다양한 퀘스트 단계들을 일관되게 처리할 수 있음 
    // 공통된 기능은 상위 클래스에서 정의, 구체적인 세부 구현은 자식 클래스에서 정의 
    private bool isFinished = false;
    private string questId; // 퀘스트의 고유 id 를 나타냄 
    private int stepIndex; // 단계 인덱스, 여러 단계 중 몇 번째인지 기록 
    public QuestState currentState = QuestState.REQUIREMENTS_NOT_MET;
    
    public void InitializeQuestStep(string questId, int stepIndex, QuestState questStepState) // 초기화하는 메서드
    {
        this.questId = questId;
        this.stepIndex = stepIndex;
        this.currentState = questStepState; 
        SetQuestStepState(questStepState); // 추상 메서드 , QuestStep 클래스를 상속한 구체적인 클래스에서 단계 상태를 설정하는 로직을 정의해야함 
    }

    protected void FinishQuestStep() // 퀘스트 단계를 완료할 때 호출하는 메소드, 이 메서드는 단계가 이미 완료된 강태인지 확인하고 그렇지 않으면 퀘스트를 완료 처리하고 해당 단계를 파괴함 
    {
        if (!isFinished) // 단게가 이미 완료된 상태인지 확인 
        {
            isFinished = true; // 퀘스트 완료 처리하고 
            GameEventsManager.instance.questEvents.AdvanceQuest(questId); // 다음 단계로 진행 시킴 
            Destroy(this.gameObject); // 해당 단게를 파괴함 
        }
    }
    
    protected void ChangeState(QuestState newState, string newStatus) // 퀘스트 단계의 상태를 변경하는 데 사용
    {
        GameEventsManager.instance.questEvents.QuestStepStateChange(
            questId, 
            stepIndex, 
            new QuestStepState(newState, newStatus)
        );// newState 와 newStatus 를 통해 퀘스트 단계의 상태를 업데이트하고 이를 gameEventsManager 에 반영
    }
    

    protected abstract void SetQuestStepState(QuestState state); // 추상 매서드, 구체적인 자식 클래스에서 단계 상태를 설정하는 구체적인 로직을 구현. 상속받는 각 클래스는 자신이 처리하는 퀘스트 단게에 맞게 이 메서드를 오버라이드하여 단게 상태 설정을 정의 

    protected abstract void SetNPCDialogue(string name, Vector2 dialogueNum);
}