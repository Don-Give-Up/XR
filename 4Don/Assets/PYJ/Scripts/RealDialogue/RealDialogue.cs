using UnityEngine;

[System.Serializable]
public class RealDialogueLine 
{
    public string speaker;  // 대사하는 사람 (NPC나 플레이어)
    public string[] dialogue; // 대사 내용
    public RealDialogueOption[] options; // 선택지들
    public TaskState currentState; // 대화를 할 때 요구하는 상태
    public TaskState changedState; // 대화가 끝나면 변경할 상태
    public Vector2 eventNum; 
    public RealDialogueLine(string speaker, string[] dialogue, TaskState currentState, TaskState changedState,RealDialogueOption[] options, Vector2 eventObjNum)
    {
        this.speaker = speaker;
        this.dialogue = dialogue;
        this.currentState = currentState;
        this.changedState = changedState; 
        this.options = options;
        this.eventNum = eventNum; 
    }
}

[System.Serializable]
public class RealDialogueOption
{
    public string optionText;  // 선택지 텍스트
    public RealDialogueLine[] nextDialogue;  // 선택 후 나올 대사
    public Vector2 optioneventNum; 
    //public Action onSelect;  // 선택 시 실행될 이벤트
    //public TaskState requiredQuestStatus; // 선택지가 활성화될 퀘스트 상태

    public RealDialogueOption(string optionText, RealDialogueLine[] nextDialogue, Vector2 eventObjNum)//, QuestState requiredQuestStatus)//, Action onSelect = null)
    {
        this.optionText = optionText;
        this.nextDialogue = nextDialogue;
        this.optioneventNum = eventObjNum; 
        //this.requiredQuestStatus = requiredQuestStatus;
        //this.onSelect = onSelect;
    }
}