using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AllDialogue
{
   [Tooltip("라인번호")] public int line;
   [Tooltip("선택라인번호")] public int selectLine;
   [Tooltip("캐릭터 이름")] public string name;
   [Tooltip("대사내용")] public string content;
   [Tooltip("이벤트")] public int choiceEventNum;
   [Tooltip("스킵라인")] public int skipLine;
   [Tooltip("퀘스트")] public string questName; 
}

[System.Serializable]
public class AllDialogueEvent
{
   public Dictionary<int, AllDialogue> alldialogues; 
}
