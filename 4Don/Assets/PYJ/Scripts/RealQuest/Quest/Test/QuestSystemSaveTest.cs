using UnityEngine;

public class QuestSystemSaveTest : MonoBehaviour
{
    [SerializeField] private Quest quest;
    [SerializeField] private Category category;
    [SerializeField] private TaskTarget target;

    private void Start()
    {
        var questSystem = QuestSystem.Instance;

        if (questSystem.ActiveQuests.Count == 0)
        {
            Debug.Log("등록");
            var newQuest = questSystem.Register(quest); 
        }
        else
        {
            questSystem.onQuestCompleted += (quest) =>
            {
                Debug.Log("성공");
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
            };
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            QuestSystem.Instance.ReceiveReport(category, target, 1);
        }
    }
}
