using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestTrackerView : MonoBehaviour
{
    [SerializeField]
    private QuestTracker questTrackerPrefab;
    [SerializeField]
    private CategoryColor[] categoryColors;

    private void Start()
    {
        QuestSystem.Instance.onQuestRegistered += CreateQuestTracker; // 새로운 이벤트가 등록되면 QuestTracker 가 만들어지도록

        foreach (var quest in QuestSystem.Instance.ActiveQuests)
            CreateQuestTracker(quest);
    }

    private void OnDestroy() // 이벤트 해제하도록
    {
        if (QuestSystem.Instance)
            QuestSystem.Instance.onQuestRegistered -= CreateQuestTracker;
    }

    private void CreateQuestTracker(Quest quest)
    {
        var categoryColor = categoryColors.FirstOrDefault(x => x.category == quest.Category);
        var color = categoryColor.category == null ? Color.white : categoryColor.color;
        Instantiate(questTrackerPrefab, transform).Setup(quest, color);
    }

    [System.Serializable]
    private struct CategoryColor // 카테고리 별로 색을 다르에 함
    {
        public Category category;
        public Color color;
    }
}
