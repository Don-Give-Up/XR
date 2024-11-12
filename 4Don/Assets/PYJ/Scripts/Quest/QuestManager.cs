
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour // 여러 퀘스트를 관리, 퀘스트 시스템 전반의 흐름을 조정하는 역할 
{
    public Dictionary<string, Quest> questDic; // 모든 데이터 저장되어 있음
    public List<string> canStartQuests;

    public static QuestManager instance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this; 
            DontDestroyOnLoad(gameObject);
        }

        canStartQuests = new List<string>();
        questDic = CreateQuestDic();
    }

    /*
    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;
        // 퀘스트 상태가 변했을 때, 단계의 상태 변화하는 것 추가. 
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;
    }
    */

    private void Start()
    {
        foreach (Quest quest in questDic.Values)
        {
            if (quest.state == QuestState.IN_PROGRESS)
            {
                // 진행하고 있는 퀘스트가 있다면 알려줘라는 의미인데... 
                // 나중에 로드랑 엮에고 나면 하면 될 듯
            }
        }
    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.state = state; 
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = true;

        foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            if (GetQuestById(prerequisiteQuestInfo.id).state != QuestState.FINISHED)
            {
                meetsRequirements = false; 
            }
        }

        return meetsRequirements; 
    }

    private void Update()
    {
        foreach (Quest quest in questDic.Values)
        {
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                Debug.Log($"준비 완료: {quest.info.id}");
                //StartQuest(quest.info.id);
                ChangeQuestState(quest.info.id, QuestState.CAN_START); 
                canStartQuests.Add(quest.info.id); // 실행가능한 친구만 추가 해놈
            }
        }
    }

    private void StartQuest(string id)
    {
        Debug.Log("퀘스트 시작함");
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(this.transform); 
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.MoveToNextStep();
        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
    }

    private void ClaimRewards(Quest quest)
    {
        // 보상 관련 이벤트 등록
    }

    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestById(id);
        ChangeQuestState(id, quest.state);
    }

    private Dictionary<string, Quest> CreateQuestDic()
    {
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quest");
        Dictionary<string, Quest> questDic = new Dictionary<string, Quest>();
        foreach (QuestInfoSO aquest in allQuests)
        {
            Debug.Log($"퀘스트: {aquest.id}"); // 퀘스트 들어감
            if (questDic.ContainsKey(aquest.id))
            {
                Debug.Log($"퀘스트 중복아이디: {aquest.id}");
            }
            questDic.Add(aquest.id, loadQuest(aquest));
        }

        return questDic; // 퀘스트 이름에 따라 정보 저장 
    }

    private Quest GetQuestById(string id)
    {
        Quest quest = questDic[id];
        if (quest == null)
        {
            Debug.Log($"해당 아이디 퀘스트 없음 {id}");
        }

        return quest;
    }
    
    private Quest loadQuest(QuestInfoSO questInfo)
    {
        // 나중에 로드된 결과로 변경
        Quest quest = new Quest(questInfo);

        return quest; 
    }
    
}
