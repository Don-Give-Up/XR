using System;
using Cysharp.Threading.Tasks;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class QuizDoor : MonoBehaviour
{
    private PhoStartGame a;
    private bool _interact = true; // >> 플레이어가 닿았을때 true, 
    public GameObject readyCanvasJjin;

    private bool OnTrigger = false;
    //콜라이더 닿았을때를 불타입변수로
    //그때 f1를 누르기
    
    private void Start()
    {
        a = PhoStartGame.Instance;
        readyCanvasJjin.SetActive(false);
        
    }

    private async UniTaskVoid Goto()
    {
        if (OnTrigger)
        {
            await UniTask.Delay(1550);
           
            OnTrigger = false;
            Debug.Log("자 노동 드가자");
            
            //readyCanvasZzab.SetActive(true); 
            //a.JoinQuiz();
            // 플레이어 텔레포트 실행
            var player = FindObjectOfType<MJPlayerMovement>();
            if (player != null && _interact )
            {
                player.Teleport();
                FirstPersonCamera.Instance.SwitchToFixedCamera();  // 모든 플레이어의 카메라를 고정 카메라로 전환
                _interact = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("닿음");
        var ntObject = other.GetComponent<NetworkObject>();
        Debug.Log($"ntObject " + ntObject);
        if (other.CompareTag("Player") && _interact)
        {
            Debug.Log($"InputAuthority : {ntObject.InputAuthority} , LocalPlayer : {PhoStartGame.Instance.runner.LocalPlayer}");
            if (ntObject.InputAuthority == PhoStartGame.Instance.runner.LocalPlayer)
            {
                
                OnTrigger = true;
                Goto().Forget();
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
                OnTrigger = false;
                Debug.Log("노동 문 멀어졌는데??");
            }
        }
    }

    private void CameraToggle()
    {
        
    }
}
