using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour // 퀘스트를 주는 쪽
{
    [SerializeField]
    private Quest[] quests;

    private void Start()
    {
        foreach (var quest in quests)
        {
            if (quest.IsAcceptable && !QuestSystem.Instance.ContainsInCompleteQuests(quest))
                QuestSystem.Instance.Register(quest);
        }
    }
}