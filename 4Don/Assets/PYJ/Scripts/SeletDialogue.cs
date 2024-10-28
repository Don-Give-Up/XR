using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SeletDialogue
{
    [Tooltip("이벤트 번호")] public int eventNum;
    
    [Tooltip("선택지")] public string choiceContent;
    
    [Tooltip("NPC 대화 시작 라인")] public int resumeNum;
    
}

[System.Serializable]
public class SeletDialogueEvent
{
    public Dictionary<int, SeletDialogue> seletDialogues;
}
