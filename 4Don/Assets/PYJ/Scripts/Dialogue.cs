using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue
{
    
    [Tooltip("캐릭터 이름")] public string npcname;

    [Tooltip("대사 내용")] public string content;

    [Tooltip("이벤트 번호")] public int eventNum;

    [Tooltip("스킵 라인")] public int skipNum;
}

[System.Serializable]
public class DialogueEvent
{
    public Dictionary<int, Dialogue> dialogues; // iD를 키 값으로 사용

    //public Dialogue[] dialogues;
}
