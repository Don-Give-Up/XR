using System;

public class QuestEvents // 이벤트를 관리하는 역할 // 엑션 델리게이트를 사용해 정의 
{
    public event Action<string> onStartQuest; // 퀘스트가 시작할 때 발생하는 이벤트
    public void StartQuest(string id) // 이 매소드에서 이 이벤트를 호출함 
    {
        if (onStartQuest != null) // 구독자가 있는지 확인함. 아무도 없다면 아무 일도 일어나지 않음 
        {
            onStartQuest(id);
        }
    }

    public event Action<string> onAdvanceQuest; // 퀘스트가 진행할 때 발생하는 이벤트 
    public void AdvanceQuest(string id)
    {
        if (onAdvanceQuest != null)
        {
            onAdvanceQuest(id);
        }
    }

    public event Action<string> onFinishQuest;
    public void FinishQuest(string id)
    {
        if (onFinishQuest != null)
        {
            onFinishQuest(id);
        }
    }

    public event Action<Quest> onQuestStateChange;
    public void QuestStateChange(Quest quest)
    {
        if (onQuestStateChange != null)
        {
            onQuestStateChange(quest);
        }
    }
    
    public event Action<string, int, QuestStepState> onQuestStepStateChange; // 퀘스트의 단계가 변경될 때 발생하는 이벤트 
    public void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        if (onQuestStepStateChange != null)
        {
            onQuestStepStateChange(id, stepIndex, questStepState);
        }
    }
    
}