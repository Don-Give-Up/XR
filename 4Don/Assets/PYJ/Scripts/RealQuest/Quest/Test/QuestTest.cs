using System;
using UnityEngine;

public class QuestSystemTest : MonoBehaviour
{
  [SerializeField] private Quest quest;
  [SerializeField] private Category category;
  [SerializeField] private TaskTarget target;

  private void Start()
  {
    var questSystem = QuestSystem.Instance;

    questSystem.onQuestRegistered += (quest) => // 등록함 
    {
      print($"New Quest: {quest.CodeName} Registered"); // 어떤 퀘스트가 등록
      print($"Active Quests Count: {questSystem.ActiveQuests.Count}"); // 몇 개나 활성화
    };

    questSystem.onQuestCompleted += (quest) => // 완료되었을 때 
    {
      print($"Quest: {quest.CodeName} Completed");
      print($"Completed Quests Count: {questSystem.CompletedQuests.Count}");

    };

    var newQuest = questSystem.Register(quest);
    newQuest.onTaskSuccessChanged += (quest, task, currentSuccess, prevSuccess) => //성공 수가 바뀌었을 떄
    {
      print($"Quest: {quest.CodeName}, Task: {task.CodeName}, CurrentSuccess: {currentSuccess}");
    };
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      QuestSystem.Instance.ReceiveReport(category, target, 1);
    }
  }
}
