using System;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class QuizDoor : MonoBehaviour
{
    private PhoStartGame a;
    private bool _interact = false; // >> 플레이어가 닿았을때 true, 
    public GameObject _1Key;

    private bool OnTrigger = false;
    //콜라이더 닿았을때를 불타입변수로
    //그때 f1를 누르기
    
    private void Start()
    {
        a = PhoStartGame.Instance;
        _1Key.SetActive(false);
    }

    private void Update()
    {
        if (OnTrigger)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _1Key.SetActive(false);
                _interact = false;
                OnTrigger = false;
                Debug.Log("자 노동 드가자");
                GotoQuiz();
                
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("닿음");
        _1Key.SetActive(true);

        _interact = true;
        if (other.gameObject.CompareTag("Player") && _interact)
        {
            OnTrigger = true;
            Debug.Log("노동 문 열어라");
        }
    }

    private void GotoQuiz()
    {
        a.Shutdown();
        a.InstantiateRunner();
        a.JoinQuiz();
    }
}
