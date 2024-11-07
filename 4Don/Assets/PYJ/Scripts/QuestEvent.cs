using UnityEngine;
using System;

public class QuestEvent : MonoBehaviour
{
    public event Action<string> onStartQuest;

    public void StartQuest(string id)
    {
        if (onStartQuest != null)
        {
            onStartQuest(id);
        }
    }
    
    public event Action<string> onAdvanceQuest;

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
    
    public event Action<string> onQuestStateChange; //수정 필요 

    public void QuestStateChange(string id)
    {
        if (onQuestStateChange != null)
        {
            onQuestStateChange(id);
        }
    }
}
