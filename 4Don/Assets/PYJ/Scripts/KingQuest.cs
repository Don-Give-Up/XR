using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KingQuest : MonoBehaviour
{
    public Queue<QuestInfoSO> quests = new Queue<QuestInfoSO>();
    public GameObject questObj;
    private TMP_Text questText;
    public int questNum = 0;

    public Action onQuestStart; // 퀘스트가 시작함
    public Action onQuestEnd; // 퀘스트가 끝남 
    
    private NPCDialogueManager npcDialogueManager;
    
    public static KingQuest instance;
    
    private void Awake()
    {
        if ( instance == null)
        {
            instance = this;
            questObj.SetActive(true);
            KingQuest.instance.onQuestStart += OnQuestStart;
            KingQuest.instance.onQuestEnd += OnQuestFinish;
            KingQuest.instance.onQuestEnd += MoveNextQuest;
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
        
        OnQuestStart();
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Alpha9)) // 마스터 키 
        {
            // 2. 소득의 주 수입원는 노동의 후 상태가 되게하기 + 보상 있는 거 적용 시켜 놓기 
            MoveNextQuest();
        }*/
    }

    //퀘스트가 끝났을 때 부르는 것
    public void MoveNextQuest() // 이 거 불리면 하나 진행, 퀘스트가 끝나면 진행한다.
    {
        // 다음 번호가 있는지 확인하고 진행해야 할 듯
        quests.Dequeue();
        quests.Dequeue();
        OnQuestStart();
        //CurrentQuestDisplay(); // 퀘스트가 옮겨지는 타이밍이랑 퀘스트가 발생하는 타이밍이랑 다름
    }
    
    // 퀘스트가 시작할 때 부르는 것 
    private void OnQuestStart()
    {
        questObj.SetActive(true);
        string questName = quests.Peek().diaplayName;
        string questContent = quests.Peek().displayContents;

        questText.text = $"{questName}\n{questContent}";
    }

    // 퀘스트가 끝날 떄 부르는 것
    public void OnQuestFinish()
    {
        questObj.SetActive(false);
    }

    public QuestInfoSO GetQuestInfo() // 여기서 현재 퀘스트의 정보를 보여주고 있음 
    {
        return quests.Peek();
    }


}
