using System;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class QuizDoor : MonoBehaviour
{
    private PhoStartGame a;
    private bool _interact = true; // >> 플레이어가 닿았을때 true, 
    public GameObject _1Key;
    public GameObject readyCanvasZzab;

    private bool OnTrigger = false;
    //콜라이더 닿았을때를 불타입변수로
    //그때 f1를 누르기
    
    private void Start()
    {
        a = PhoStartGame.Instance;
        readyCanvasZzab.SetActive(false);
        _1Key.SetActive(false);
    }

    private void Update()
    {
        if (OnTrigger)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _1Key.SetActive(false);
                _interact = false;
                OnTrigger = false;
                Debug.Log("자 노동 드가자");
                readyCanvasZzab.SetActive(true); 
                a.JoinQuiz();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("닿음");
        if (other.gameObject.CompareTag("Player") && _interact)
        {
            var ntObject = other.GetComponent<NetworkObject>();
            if (ntObject.InputAuthority != PhoStartGame.Instance.runner.LocalPlayer)
            {
                _1Key.SetActive(true);
                OnTrigger = true;
                Debug.Log("노동 문 열어라");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var ntObject = other.GetComponent<NetworkObject>();
            if (ntObject.InputAuthority != PhoStartGame.Instance.runner.LocalPlayer)
            {
                _1Key.SetActive(false);
                OnTrigger = false;
                Debug.Log("노동 문 멀어졌는데??");
            }
        }
    }
}
