using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour // 게임에서 발생하는 여러 이벤트를 관리하는 클래스 
{
    public static GameEventsManager instance { get; private set; } // 싱글톤 패턴을 사용 , 외부에서 인스턴스를 수정하지 못하도록 보호 
    
    //public QuestEvents questEvents;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;

        // 각종 인스턴스 초기화 
        // initialize all events
        //questEvents = new QuestEvents();
    }
}