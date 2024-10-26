using UnityEngine;

public class PlayerSoloController : MonoBehaviour
{
    

     public float moveSpeed = 5f; // 이동 속도

     void Update()
     {
         // 좌우 방향키 입력 받기
         float moveHorizontal = Input.GetAxis("Horizontal");

         // 방향 전환
         transform.Rotate(0, moveHorizontal * moveSpeed * Time.deltaTime * 100f, 0);

         // 앞뒤 이동 입력 받기
         float moveVertical = Input.GetAxis("Vertical");

         // 이동 벡터 생성
         Vector3 movement = transform.forward * moveVertical;

         // 플레이어 이동
         transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
     }
    
}