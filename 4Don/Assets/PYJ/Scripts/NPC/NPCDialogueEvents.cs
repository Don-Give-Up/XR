using System;
using UnityEngine;

public class NPCDialogueEvents
{

    public event Action<string> onDialogueNumCheck;

    public void CheckDialogue(string npcName)
    {
        if (onDialogueNumCheck != null)
        {
            onDialogueNumCheck(npcName);
        }
    }

    public event Action<Vector2> onShowDialoge; // 퀘스트가 시작할 때 발생하는 이벤트
    public void ShowDialoge(Vector2 dialogueNum) // 이 매소드에서 이 이벤트를 호출함 
    {
        if (onShowDialoge != null) // 구독자가 있는지 확인함. 아무도 없다면 아무 일도 일어나지 않음 
        {
            onShowDialoge(dialogueNum);
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

    public event Action<Vector2> onShowSelectDialogue; 
    public void AdvanceSelectDialogue(Vector2 dialogueNum)
    {
        if (onShowSelectDialogue != null)
        {
            onShowSelectDialogue(dialogueNum);
        }
    }
    
}
