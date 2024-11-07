using UnityEngine;

public class Quest
{
   public QuestInfoSO info;

   public QuestState state;
   private int currentQuestStepIndex;

   public Quest(QuestInfoSO questInfo)
   {
      this.info = questInfo;
      this.state = QuestState.REQUIREMENTS_NOT_MET;
      this.currentQuestStepIndex = 0; 
   }

   public void MoveToNextStep()
   {
      currentQuestStepIndex++;
   }

   public bool CurrentStepExists()
   {
      return (currentQuestStepIndex < info.questStepPrefabs.Length);
   }

   public void InstantiateCurrentQuestStep(Transform parentTransform)
   {
      GameObject questStepPrefab = GetCurrentQuestStepPrefab();
      if (questStepPrefab != null)
      {
         Object.Instantiate<GameObject>(questStepPrefab, parentTransform);
      }
   }

   private GameObject GetCurrentQuestStepPrefab()
   {
      GameObject questStepPrefab = null;
      if (CurrentStepExists())
      {
         questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
      }
      else
      {
         Debug.Log("현재 순서의 퀘스트 없엉");
      }

      return questStepPrefab;
   }
}
