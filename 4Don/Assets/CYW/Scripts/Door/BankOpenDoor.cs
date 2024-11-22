using System;
using UnityEngine;

public class BankOpenDoor : MonoBehaviour
{
    //// 오른쪽 문이든 왼쪽이 문이든 클릭하면 둘 다 열리게
    // 문 클릭하면 오른쪽, 왼쪽 문 애니메이션 모두 재생
    

    public Animator leftDoorAnim;
    public Animator rightDoorAnim;

    private void Update()
    {
        // 마우스 좌 클릭 시
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭 위치를 Raycast로 가져옴
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 클릭한 물체 태그가 문이라면
                if (hit.collider.CompareTag("Door"))
                {
                    
                    Debug.Log("문 클릭");
                    // 두 문을 동시에 열게 트리거 실행
                    leftDoorAnim.SetTrigger("Open");
                    rightDoorAnim.SetTrigger("Open");
                }
            }
        }
    }
}
