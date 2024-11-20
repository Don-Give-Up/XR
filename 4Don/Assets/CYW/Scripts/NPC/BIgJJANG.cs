using System;
using UnityEngine;

public class BigJJANG : MonoBehaviour
{
    // 클릭하면 대화창이랑 짱 크게 나오는 거
    // 대사창은 언니가 크게 하니까 여기선 크게만 내기
    // RawImage를 끄고 키면 될 듯

    public GameObject bigJJANG;   // 대화창이나 짱이 나오는 오브젝트
    public Animator anim;         // 애니메이터 컴포넌트
    
    public string jjangTag = "NPC"; // 짱 태그 누르면 켜졌다 꺼졌다 하게

    private void Start()
    {
        // 게임 시작 시 빅짱 비활성화
        if (bigJJANG != null)
        {
            bigJJANG.SetActive(false);
            Debug.Log("빅짱 비활성화");
        }

        // 애니메이터 초기화
        if (anim == null)
        {
            anim = bigJJANG.GetComponent<Animator>();
        }
    }

    private void Update()
    {
        // 마우스 클릭 시
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 위치에서 Raycast 발사
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Raycast로 클릭된 오브젝트 확인
            if (Physics.Raycast(ray, out hit))
            {
                // 클릭한 오브젝트가 "NPC" 태그가 맞는지 확인
                if (hit.collider.CompareTag(jjangTag))
                {
                    if (bigJJANG != null)
                    {
                        // bigJJANG 오브젝트를 켜거나 끄기
                        bool isActive = !bigJJANG.activeSelf;
                        bigJJANG.SetActive(isActive);

                        // 애니메이션 파라미터 "IsTalking"을 설정
                        if (anim != null)
                        {
                            anim.SetBool("IsTalking", isActive); // 활성화되면 true, 비활성화되면 false
                        }
                    }
                }
            }
        }
    }
}
