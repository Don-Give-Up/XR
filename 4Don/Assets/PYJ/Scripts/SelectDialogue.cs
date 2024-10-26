using UnityEngine;

[System.Serializable]
public class SelectDialogue
 
{

    [Tooltip("대사 내용")]
    public string[] contexts;

    [Tooltip("옮길라인")]
    public string[] moveNum;
    
    //대화 내용 
    //옮길 라인등에 대한 파라메타를 추가

}

[System.Serializable]
public class SelectEvent
{
    //이벤트 이름
    public string name;

    //public Vector2 line;
    public SelectDialogue[] Selecter;

}
