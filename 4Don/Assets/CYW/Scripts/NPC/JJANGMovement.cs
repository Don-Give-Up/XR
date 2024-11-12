using Fusion;
using UnityEngine;
using UnityEngine.AI;

public class JJANGMovement : NetworkBehaviour
{
    public Transform player; // 플레이어의 Transform
    private NavMeshAgent agent; // NavMeshAgent

    public GameObject jjang;

    public float distanceAheadPlayer = 1f; // 플레이어의 앞쪽으로 이동할 거리
    public float offsetDistance = 1f; // 플레이어의 오른쪽에 위치할 거리

    public override void Spawned()
    {
        // NavMeshAgent 컴포넌트를 가져옵니다.
        agent = GetComponent<NavMeshAgent>();
    }

    public override void FixedUpdateNetwork()
    {
        if (player != null)
        {
            // 플레이어의 앞쪽 벡터 방향으로 이동하도록 목표 위치 설정
            Vector3 offsetPosition = player.position + player.forward * distanceAheadPlayer + player.right * offsetDistance;

            // 목표 위치로 NPC 이동
            agent.SetDestination(offsetPosition);
            //agent.isStopped
            //

            // NPC가 멈췄을 때 플레이어를 마주보게 하는 코드
            if (agent.velocity.sqrMagnitude == 0)  // 속도가 0일 때, 즉 멈췄을 때
            {
                // 플레이어를 바라보게 회전
                Vector3 direction = player.position - transform.position;
                direction.y = 0; // Y축 회전만 하도록 함 (수평 회전만 필요)
                Quaternion toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * 500f);  // 회전 속도 조절
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            jjang.SetActive(false);
        }
        
        
        
    }
    
    
    
    
    
}
