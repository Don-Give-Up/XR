using System;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizDoor : MonoBehaviour
{
    private PhoStartGame a;
    private bool _interact = true;
    
    private void Start()
    {
        a = PhoStartGame.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("닿음");
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.F1) && _interact)
        {
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
