using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    private RealDialogueManager dialogueManager;
    private Task task;  // 퀘스트

    private void Start()
    {
        dialogueManager = FindObjectOfType<RealDialogueManager>();  // 대화 관리자 찾기
    }

    // NPC와 상호작용 시 호출되는 메서드
    public void InteractWithNPC()
    {
        if (task != null)
        {
            dialogueManager.SetTask(task);  // 퀘스트 설정
            //task.Owner.UpdateState(); // 퀘스트 상태 갱신 (대화 시점에 상태 확인)
        }
    }

    public void SetTask(Task task)
    {
        this.task = task;
    }
}