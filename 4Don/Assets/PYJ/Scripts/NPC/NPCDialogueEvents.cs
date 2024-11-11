using System;
using UnityEngine;

public class NPCDialogueEvents
{
    public event Action<string> onStartDialoge; // 퀘스트가 시작할 때 발생하는 이벤트
    public void StartDialoge(string npcName) // 이 매소드에서 이 이벤트를 호출함 
    {
        if (onStartDialoge != null) // 구독자가 있는지 확인함. 아무도 없다면 아무 일도 일어나지 않음 
        {
            onStartDialoge(npcName);
        }
    }

    public event Action<Vector2> onAdvanceDialogue; 
    public void AdvanceDialoge(Vector2 dialogueNum)
    {
        if (onAdvanceDialogue != null)
        {
            onAdvanceDialogue(dialogueNum);
        }
    }

    public event Action<Vector2> onAdvanceSelectDialogue; 
    public void AdvanceSelectDialogue(Vector2 dialogueNum)
    {
        if (onAdvanceDialogue != null)
        {
            onAdvanceDialogue(dialogueNum);
        }
    }
    
    public event Action onFinishDialogue;
    public void FinishQuest()
    {
        if (onFinishDialogue != null)
        {
            onFinishDialogue();
        }
    }

    // 일단 좀 있다가
    public event Action<Quest> onDialogueStateChange;
    public void DialogueStateChange(Quest quest)
    {
        if (onDialogueStateChange != null)
        {
            onDialogueStateChange(quest);
        }
    }
    
    public event Action<Quest> onSelectDialogueStateChange;
    public void SelectDialogueStateChange(Quest quest)
    {
        if (onSelectDialogueStateChange != null)
        {
            onSelectDialogueStateChange(quest);
        }
    }
    
    // 선택 이벤트는 끝날 때 한 번 확인해서 떠야되는 이벤트 있음 뜨게 하고 이게 꺼지면 다시 진행상태로 돌아오게 하기(alpha 값 조절하면 될 것 같음)
    //public event Action<int, string> 
}
