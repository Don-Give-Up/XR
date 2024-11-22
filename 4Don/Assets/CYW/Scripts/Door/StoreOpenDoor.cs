using UnityEngine;

public class StoreOpenDoor : MonoBehaviour
{
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
