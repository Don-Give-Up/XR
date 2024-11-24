using System;
using UnityEngine;

public class BankOpenDoor : MonoBehaviour
{
    //// 오른쪽 문이든 왼쪽이 문이든 클릭하면 둘 다 열리게
    // 문 클릭하면 오른쪽, 왼쪽 문 애니메이션 모두 재생


    public Animator leftDoorAnim;
    public Animator rightDoorAnim;

  

    private void OnTriggerEnter(Collider other)
    {
      
        
        // 충돌한 물체의 태그가 "Door"라면
        if (other.CompareTag("Player"))
        {
            Debug.Log("문과 충돌");
            // 두 문을 동시에 열게 트리거 실행
            leftDoorAnim.SetTrigger("Open");
            rightDoorAnim.SetTrigger("Open");
        }
    }
}
