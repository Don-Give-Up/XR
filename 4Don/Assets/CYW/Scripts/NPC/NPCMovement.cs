using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public Transform[] waypoints; // 각 NPC의 이동 경로
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private bool isWaiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToNextWaypoint();
    }

    void Update()
    {
        // 목적지에 도달했는지 확인
        if (!isWaiting && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            StartCoroutine(WaitAtDestination());
        }
    }

    // 목적지에 도달한 후 3초 동안 대기
    private IEnumerator WaitAtDestination()
    {
        isWaiting = true; // 멈춤 상태
        yield return new WaitForSeconds(3f); // 3초 동안 대기
        MoveToNextWaypoint(); // 새로운 목적지로 이동
        isWaiting = false;
    }

    // 다음 목적지로 이동
    private void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        agent.SetDestination(waypoints[currentWaypointIndex].position);

        // 다음 목적지로 인덱스를 순차적으로 변경
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

}
