using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KingQuest : MonoBehaviour
{
    public Queue<QuestInfoSO> quests = new Queue<QuestInfoSO>();
    public GameObject questObj;
    private TMP_Text questText;
    public static KingQuest instance;
    
    private void Awake()
    {
        if ( instance == null)
        {
            instance = this; 
        }
        else
        {
            Destroy(gameObject);
        }

        questText = questObj.GetComponentInChildren<TMP_Text>();
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("KingQuest"); //모든 퀘스트 다 불러와
        
        foreach (var quest in allQuests)
        {
            Debug.Log($"{quest.id}");
            quests.Enqueue(quest);
        }
        
        CurrentQuestDisplay();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9)) // 마스터 키 
        {
            // 2. 소득의 주 수입원는 노동의 후 상태가 되게하기 + 보상 있는 거 적용 시켜 놓기 
            MoveNextQuest();
        }
    }

    // 이 코드는 
    public void MoveNextQuest() // 이 거 불리면 하나 진행, 퀘스트가 끝나면 진행한다.
    {
        // 다음 번호가 있는지 확인하고 진행해야 할 듯
        quests.Peek();
        quests.Dequeue();
        CurrentQuestDisplay();
    }
    
    private void CurrentQuestDisplay()
    {
        string questName = quests.Peek().diaplayName;
        string questContent = quests.Peek().displayContents;

        questText.text = $"{questName}\n{questContent}";
    }

    public QuestInfoSO GetQuestInfo()
    {
        return quests.Peek();
    }


}
