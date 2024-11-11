using System;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizDoor : MonoBehaviour
{
    private PhoStartGame a;
    private bool _interact = true;
    public GameObject f1Key;
    //콜라이더 닿았을때를 불타입변수로
    //그때 f1를 누르기
    
    private void Start()
    {
        a = PhoStartGame.Instance;
    }

    private void Update()
    {
        // if(Input.GetKeyDown(KeyCode.f1))
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("닿음");
        f1Key.SetActive(true);
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.F1) && _interact)
        {
            f1Key.SetActive(false);
            _interact = false;
            Debug.Log("노동 문 열어라");

            GotoQuiz();
        }
    }

    private void GotoQuiz()
    {
        a.Shutdown();
        a.InstantiateRunner();
        a.JoinQuiz();
    }
}
