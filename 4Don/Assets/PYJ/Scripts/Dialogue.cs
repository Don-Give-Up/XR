using UnityEngine;


//인스펙터창에서 수정가능
[System.Serializable]
public class Dialgoue  // 개별 대화 내용 정의
{

    [Tooltip("캐릭터 이름")]
    public string name;

    [Tooltip("대사 내용")]
    public string[] contexts;

    [Tooltip("이벤트 번호")]
    public string[] number;

    [Tooltip("스킵라인")]
    public string[] skipnum;

}

[System.Serializable]
public class DialgoueEvent // 대화 이벤트 정의 
{

    //이벤트 이름
    public string name;

    //public Vector2 line;
    public Dialgoue[] dialgoues;

}
