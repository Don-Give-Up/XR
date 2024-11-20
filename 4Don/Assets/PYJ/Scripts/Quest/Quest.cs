using UnityEngine;

public class Quest // 개별 퀘스트의 세부 사항과 진행 상태 관리 , 데이터 관리 클래스
{
  // 선언
  public QuestInfoSO info;

  public QuestState state;
  private int currentQuestStepIndex;
  private QuestStepState[] questStepStates;

  // 생성
  public Quest(QuestInfoSO questInfo)
  {
    this.info = questInfo;
    this.state = QuestState.REQUIREMENTS_NOT_MET;
    this.currentQuestStepIndex = 0;
    this.questStepStates = new QuestStepState[info.questStepPrefabs.Length];
    for (int i = 0; i < questStepStates.Length; i++)
    {
      questStepStates[i] = new QuestStepState();
    }
  }

  public void MoveToNextStep()
  {
    Debug.Log($"현재 퀘스트스텝 인덱스 : {currentQuestStepIndex}");
    currentQuestStepIndex++;
  }

  public bool CurrentStepExists()
  {
    return (currentQuestStepIndex < info.questStepPrefabs.Length);
  }

  public void InstantiateCurrentQuestStep(Transform parentTransform) // 현재 퀘스트 단계를 나타내는 게임 오브젝트를 씬에 동적으로 생성
  {
    GameObject questStepPrefab = GetCurrentStepPrefab();
    if (questStepPrefab != null)
    {
      QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>();
      questStep.InitializeQuestStep(info.id, currentQuestStepIndex, questStepStates[currentQuestStepIndex].state);// 생성된 오브젝트에는 QuestStep 이 있고 각
    }
  }

  private GameObject GetCurrentStepPrefab()
  {
    GameObject questStepPrefab = null;
    if (CurrentStepExists())
    {
      questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
    }
    else
    {
      Debug.Log($"현재 퀘스트 단계가 존재하지 않음, ID: {info.id}, StepIndex: {currentQuestStepIndex}");
    }

    return questStepPrefab;
  }

  /*
  public string GetFullStatusText()
  {
    string fullStatus = "";

    if (state == QuestState.REQUIREMENTS_NOT_MET)
    {
      fullStatus = "Requirements are not yet met to start this quest.";
    }
    else if (state == QuestState.CAN_START)
    {
      fullStatus = "This quest can be started!";
    }
    else 
    {
      // display all previous quests with strikethroughs
      for (int i = 0; i < currentQuestStepIndex; i++)
      {
        fullStatus += "<s>" + questStepStates[i].status + "</s>\n";
      }
      // display the current step, if it exists
      if (CurrentStepExists())
      {
        fullStatus += questStepStates[currentQuestStepIndex].status;
      }
      // when the quest is completed or turned in
      if (state == QuestState.CAN_FINISH)
      {
        fullStatus += "The quest is ready to be turned in.";
      }
      else if (state == QuestState.FINISHED)
      {
        fullStatus += "The quest has been completed!";
      }
    }

    return fullStatus;
  }
  */
  
}
