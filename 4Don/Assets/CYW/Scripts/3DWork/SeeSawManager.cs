using System;
using System.Collections;
using Fusion;
using Fusion.Addons.Physics;
using UnityEngine;
using UnityEngine.UI;

public class SeeSawManager : NetworkBehaviour
{
    // 정답 선택 상태를 추적하는 변수
    private string selectedAnswer = ""; // "O" 또는 "X"로 설정

// O, X 버튼 클릭을 처리할 Button 컴포넌트
    public Button buttonO;
    public Button buttonX;

    public float pauseTime = 5f;

    private NetworkRigidbody3D rb;
    private bool _interact = false;

    private void Awake()
    {
        rb = GetComponent<NetworkRigidbody3D>();
    }

    private void FixedUpdate()
    {
       // Debug.Log($"fixedupdate rot : {rb.RBRotation.eulerAngles}");
    }

    public override void FixedUpdateNetwork()
    {
      //  Debug.Log($"FixedUpdateNetwork rot : {rb.RBRotation.eulerAngles}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 객체의 태그가 "GroundO"일 경우
        if (collision.gameObject.CompareTag("GroundO"))
        {
            if (_interact)
                return;

            _interact = true;
            
            Debug.Log("GroundO 지면입니다");
            selectedAnswer = "O"; // 선택된 답을 "O"로 설정
            
            Debug.Log("O 버튼을 클릭합니다.");
            buttonO.onClick.Invoke(); // O 버튼 클릭 트리거
            
            StartCoroutine(CollisionProcess());
        }
        // 충돌한 객체의 태그가 "GroundX"일 경우
        else if (collision.gameObject.CompareTag("GroundX"))
        {
            if (_interact)
                return;

            _interact = true;
            
            Debug.Log("GroundX 지면입니다");
            selectedAnswer = "X"; // 선택된 답을 "X"로 설정
            
            Debug.Log("X 버튼을 클릭합니다.");
            buttonX.onClick.Invoke(); // X 버튼 클릭 트리거

            StartCoroutine(CollisionProcess());
        }
    }

    private IEnumerator CollisionProcess()
    {
        rb.RBIsKinematic = true;
        
        yield return new WaitForSeconds(pauseTime);
        
        rb.RBRotation = Quaternion.Euler(270f, rb.RBRotation.eulerAngles.y, rb.RBRotation.eulerAngles.z);
        rb.RBIsKinematic = false;

        var players = GameObject.FindObjectsByType<MJPlayerMovement>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach (var player in players)
        {
            player.Teleport(new Vector3(537f, 35f, 179f), true);
        }

        _interact = false;
    }

    public void TriggerCollision()
    {
        Debug.Log("해설 후 충돌 발생!");
    }
}