using UnityEngine;
using UnityEngine.AI;

public class JJANGMovement : MonoBehaviour
{
   public Transform player; // 플레이어의 Transform
    private NavMeshAgent agent; // NavMeshAgent

    public float distanceBehindPlayer = 2f; // 플레이어와의 거리
    public float offsetDistance = 2f; // 플레이어의 오른쪽에 위치할 거리

    void Start()
    {
        // NavMeshAgent 컴포넌트를 가져옵니다.
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null)
        {
            // 플레이어의 오른쪽 벡터 방향으로 이동하도록 목표 위치 설정
            Vector3 offsetPosition = player.position + player.right * offsetDistance;

            // 목표 위치로 NPC 이동
            agent.SetDestination(offsetPosition);
        }
    }
}
