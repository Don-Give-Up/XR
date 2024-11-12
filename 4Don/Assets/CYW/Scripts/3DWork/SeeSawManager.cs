using System;
using UnityEngine;
using UnityEngine.UI;

public class SeeSawManager : MonoBehaviour
{
    // 정답 선택 상태를 추적하는 변수
    private string selectedAnswer = ""; // "O" 또는 "X"로 설정

// O, X 버튼 클릭을 처리할 Button 컴포넌트
    public Button buttonO;
    public Button buttonX;


    //private bool canCollide = false;
    
    private void OnCollisionEnter(Collision collision)
    {

        
        // 충돌한 객체의 태그가 "GroundO"일 경우
        if (collision.gameObject.CompareTag("GroundO"))
        {
            Debug.Log("GroundO 지면입니다");
            selectedAnswer = "O"; // 선택된 답을 "O"로 설정
            
            Debug.Log("O 버튼을 클릭합니다.");
            buttonO.onClick.Invoke(); // O 버튼 클릭 트리거
           
        }
        // 충돌한 객체의 태그가 "GroundX"일 경우
        else if (collision.gameObject.CompareTag("GroundX"))
        {
            Debug.Log("GroundX 지면입니다");
            selectedAnswer = "X"; // 선택된 답을 "X"로 설정
            
            Debug.Log("X 버튼을 클릭합니다.");
            buttonX.onClick.Invoke(); // X 버튼 클릭 트리거
           
        }
    }

    public void TriggerCollision()
    {
        Debug.Log("해설 후 충돌 발생!");
    }

    
    
    


   
}
