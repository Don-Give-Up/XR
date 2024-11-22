using System;
using UnityEngine;
using UnityEngine.AI;

public class BigJJANG : MonoBehaviour
{
    // 클릭하면 대화창이랑 짱 크게 나오는 거
    // 대사창은 언니가 크게 하니까 여기선 크게만 내기
    // RawImage를 끄고 키면 될 듯

    // 물음표 껐다 켜기

    public GameObject bigJJANG; // 대화창이나 짱이 나오는 오브젝트
    public Animator anim; // 애니메이터 컴포넌트
    public GameObject mark;
    public NavMeshAgent agent; // NavMeshAgent
    public string jjangTag = "NPC"; // 짱 태그 누르면 켜졌다 꺼졌다 하게

    public BigJJANGMovement bigJjangMovement;

    private void Start()
    {
        // 게임 시작 시 빅짱 비활성화
        if (bigJJANG != null)
        {
            bigJJANG.SetActive(false);
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
            anim = bigJJANG.GetComponent<Animator>();
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
                if (hit.collider.CompareTag(jjangTag))
                {
                    if (bigJJANG != null)
                    {
                        // bigJJANG 오브젝트를 켜거나 끄기
                        bool isActive = !bigJJANG.activeSelf;
                        bigJJANG.SetActive(isActive);

                        // 물음표 오브젝트 끄기
                        mark.SetActive(false);


                        // 애니메이션 파라미터 "IsTalking"을 설정
                        if (anim != null)
                        {
                            anim.SetBool("IsTalking", isActive); // 활성화되면 true, 비활성화되면 false
                            // 말하는 중일 때 WayPoint로 이동하지 않게
                           
                            //말하는 중일 때 NPC가 그 자리에 멈추도록
                            if (isActive)
                            {
                                agent.isStopped = true;
                            }
                            else
                            {
                                agent.isStopped = false;
                                bigJjangMovement.ToNextWaypoint();
                            }
                            
                        }
                    }
                }
            }
        }
        
        
        // NavMeshAgent의 속도가 0보다 크면 이동 중, 아니면 멈춤
        if (agent.velocity.sqrMagnitude > 0f) // 
        {
            anim.SetBool("IsWalking", true); // 걷기 애니메이션 시작
        }
        else
        {
            anim.SetBool("IsWalking", false); // 입력이 없을 때 걷기 애니메이션 멈춤
        }
        
        
    }

    
    
    public void QuestionMark()
    {
        // 퀘스트 바뀌거나 할 말 있을 때 켜기
        // 지금은 1번 누르면 켜지게
        if (Input.GetKeyDown(KeyCode.Alpha1))
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
