using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestReporter : MonoBehaviour
{
    [SerializeField]
    private Category category;
    [SerializeField]
    private TaskTarget target;
    [SerializeField]
    private int successCount;
    [SerializeField]
    private string[] colliderTags;

    private void OnTriggerEnter(Collider other)
    {
        ReportIfPassCondition(other);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReportIfPassCondition(collision);
    }

    public void Report()
    {
        //Debug.Log($"보고하는 중! category; {category}, target: {target}, successCount: {successCount}");
        QuestSystem.Instance.ReceiveReport(category, target, successCount);
    }
    
    // 내가 하고 싶은 건 Quest 에 

    private void ReportIfPassCondition(Component other)
    {
        if (colliderTags.Any(x => other.CompareTag(x)))
            Report();
    }
}
