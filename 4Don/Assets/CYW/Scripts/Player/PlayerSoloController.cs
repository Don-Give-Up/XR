using System;
using UnityEngine;

public class PlayerSoloController : MonoBehaviour
{
    
    public float moveSpeed = 5f; // 이동 속도
    public float rotateSpeed = 2.5f; // 이동 속도
    public Animator anim; // Animator 컴포넌트를 연결할 변수
    private float epsilon = 0.01f; // 아주 작은 값

    
    /*// 다 빠져야함 
    //public GameObject stock1;
    //public GameObject stock2;
    //public GameObject dialogue;
    */

    void Update()
    {
        // 좌우 방향키 입력 받기
        float moveHorizontal = Input.GetAxis("Horizontal");

        // 방향 전환
        transform.Rotate(0, moveHorizontal * rotateSpeed * Time.deltaTime * 100f, 0);

        // 앞뒤 이동 입력 받기
        float moveVertical = Input.GetAxis("Vertical");

        // 이동 벡터 생성
        Vector3 movement = transform.forward * moveVertical;

        // 플레이어 이동
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        
        // 애니메이션 제어: 위아래 방향키 입력이 있을 때만 걷기 애니메이션 실행
        if (Mathf.Abs(moveVertical) > epsilon) // 위아래 방향키 입력이 있을 때
        {
            anim.SetBool("IsWalk", true); // 걷기 애니메이션 시작
        }
        else if (Mathf.Abs(moveHorizontal) > epsilon) // 좌우 방향키 입력이 있을 때
        {
            anim.SetBool("IsWalk", false); // 걷기 애니메이션 멈춤
        }
        else
        {
            anim.SetBool("IsWalk", false); // 입력이 없을 때 걷기 애니메이션 멈춤
        }

        /*//다 빠져야함 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            stock1.SetActive(true);
            stock2.SetActive(false);
            dialogue.SetActive(false);
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            stock1.SetActive(false);
            stock2.SetActive(true);
            dialogue.SetActive(false);
        }*/
    }
    
}