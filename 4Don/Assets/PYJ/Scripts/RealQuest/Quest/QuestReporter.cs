using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
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

    private bool isNPCInteraction = false; 

    private void Start()
    {
        // 일단은 이 타이밍에 , 원래는 NPC 를 클릭하는 타이밍
        GameEventsManager.instance.npcdialogEvents.onShow += OnNPCDialogue;
    }

    private void OnNPCDialogue()
    {
        isNPCInteraction = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        ReportIfPassCondition(other);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReportIfPassCondition(collision);
    }
    
    // 일단 1번은 이렇게 해도 될 것 같은데 여러번이면 어떻게 하지? 
    // UniTask로 isNPCInteraction이 true가 될 때까지 기다리는 메서드
    public async UniTask WaitForNPCInteractionAsync()
    {
        // isNPCInteraction이 true일 때까지 기다림
        await UniTask.WaitUntil(() => isNPCInteraction);
        
        isNPCInteraction = false; 
        
        Debug.Log($"category; {category}, target: {target}, successCount: {successCount}");
        QuestSystem.Instance.ReceiveReport(category, target, successCount);
    }
    public void Report()
    {
        Debug.Log($"category; {category}, target: {target}, successCount: {successCount}");
        QuestSystem.Instance.ReceiveReport(category, target, successCount);
    }

    private void ReportIfPassCondition(Component other)
    {
        if (colliderTags.Any(x => other.CompareTag(x)))
            Report();
    }
}
