using UnityEngine;
using UnityEngine.Serialization;

public class BIgHANG : MonoBehaviour
{
    public GameObject bigHANG; // 대화창이나 짱이 나오는 오브젝트
    public GameObject bigHANGLight;
    public Animator anim; // 애니메이터 컴포넌트
    public GameObject mark;
    public string hangTag = "HANG"; // 짱 태그 누르면 켜졌다 꺼졌다 하게
    

    private void Start()
    {
        // 게임 시작 시 빅짱 비활성화
        if (bigHANG != null)
        {
            bigHANG.SetActive(false);
            bigHANGLight.SetActive(false);
            Debug.Log("빅짱 비활성화");
        }

        if (mark != null)
        {
            mark.SetActive(false);
            Debug.Log("마크 비활성화");
        }


        // 애니메이터 초기화
        if (anim == null)
        {
            anim = bigHANG.GetComponent<Animator>();
        }
    }

    private void Update()
    {
        QuestionMark();

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
                if (hit.collider.CompareTag(hangTag))
                {
                    if (bigHANG != null)
                    {
                        // bigJJANG 오브젝트를 켜거나 끄기
                        bool isActive = !bigHANG.activeSelf;
                        bigHANG.SetActive(isActive);
                        bigHANGLight.SetActive(isActive);

                        // 물음표 오브젝트 끄기
                        mark.SetActive(false);


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

    
    
    public void QuestionMark()
    {
        // 퀘스트 바뀌거나 할 말 있을 때 켜기
        // 지금은 1번 누르면 켜지게
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mark.SetActive(true);
        }
    }

    // 물음표가 생성되고 안 되고를 판단하는 메소드 return값을 받아와야 다른 스크립트에 쓸 수 있
    public bool MarkState()
    {
        return mark.activeSelf;
        
    }
}
