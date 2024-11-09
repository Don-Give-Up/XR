using UnityEngine;

public class ParticleScale : MonoBehaviour
{
    public GameObject imageObject;  // 이미지 오브젝트 (이것이 활성화되면 스케일을 증가)
    public GameObject targetObject; // 스케일을 변경할 대상 오브젝트
    public GameObject newObject;   // 새로운 게임 오브젝트 (스케일이 4 이상일 때 추가될 오브젝트)

    private float currentScale = 0.5f;  // 현재 스케일 값, 초기값은 0.5
    private bool imageWasActivated = false;  // 이미지 오브젝트가 활성화된 적이 있는지 확인
    private bool newObjectAdded = false;    // 새로운 오브젝트가 추가된 적이 있는지 확인

// 새로운 오브젝트의 고정된 위치
    private Vector3 newObjectPosition = new Vector3(232.37f, 46.2f, 381f);

    void Update()
    {
        // 이미지 오브젝트가 활성화되면
        if (imageObject.activeSelf && !imageWasActivated)
        {
            // 이미지가 한 번 활성화된 후에는 스케일을 1만큼 증가
            if (currentScale < 4f)
            {
                currentScale += 1f;  // 스케일을 1 증가
                targetObject.transform.localScale = new Vector3(currentScale, currentScale, currentScale);

                // 스케일이 4 이상으로 커지지 않도록 제한
                if (currentScale >= 4f)
                {
                    currentScale = 4f; // 목표 스케일 4에 고정
                    // 스케일이 4 이상이 되면 새로운 게임 오브젝트 추가
                    if (!newObjectAdded && newObject != null)
                    {
                        // 고정된 위치에 새로운 오브젝트 추가
                        Instantiate(newObject, newObjectPosition, Quaternion.identity);
                        newObjectAdded = true;  // 새로운 오브젝트가 추가된 것을 기록
                        Debug.Log("새로운 오브젝트가 추가되었습니다.");
                    }

                    // 디버그 로그 추가: 스케일이 증가할 때마다 출력
                    Debug.Log("불꽃 스케일 증가: " + currentScale);

                    // 스케일을 원래 값으로 되돌리기
                    currentScale = 0.5f;
                    targetObject.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
                }
            }

            // 이미지 오브젝트가 한 번 활성화되었음을 기록
            imageWasActivated = true;
        }
        else if (!imageObject.activeSelf)
        {
            // 이미지 오브젝트가 비활성화되면, 다시 활성화될 때까지 기다림
            imageWasActivated = false;
            newObjectAdded = false;  // 다시 새로운 오브젝트를 추가할 수 있도록 리셋
        }
    }
    
    // 바구니에 동전을 준다거나 선생님 쿠폰을 랜덤으로 하나 지급하거나 환급의 개념처럼 일정 금액을 주거나!
    // 현재는 보물상자가 나타나는 기능으로 구현
    
    
}
