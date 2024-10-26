using UnityEngine;

public class ButtonGo : MonoBehaviour
{
    
    public GameObject targetObject1; // 첫 번째 버튼이 활성화할 오브젝트
    public GameObject targetObject2; // 두 번째 버튼이 활성화할 오브젝트

    // 첫 번째 버튼 클릭 시 호출되는 메소드
    public void OnButton1Click()
    {
        if (targetObject1 != null)
        {
            targetObject1.SetActive(!targetObject1.activeSelf); // 첫 번째 오브젝트 활성화/비활성화
        }
    }

    // 두 번째 버튼 클릭 시 호출되는 메소드
    public void OnButton2Click()
    {
        if (targetObject2 != null)
        {
            targetObject2.SetActive(!targetObject2.activeSelf); // 두 번째 오브젝트 활성화/비활성화
        }
    }
    
}
