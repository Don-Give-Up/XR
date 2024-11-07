using System;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance { get; private set; }

    public QuestEvent questEvent;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("게임 이벤트 매니저 두개임");
        }

        instance = this;

        questEvent = new QuestEvent(); // 퀘스트 인스턴스 초기화
    }
}
